namespace TipsAndTricksLibrary.Tests.DbCommands
{
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using TipsAndTricksLibrary.DbCommands;
    using Xunit;

    public class TvpParameterTests
    {
        private class TestEntity
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int? Value { get; set; }

            protected bool Equals(TestEntity other)
            {
                return Id == other.Id && string.Equals(Name, other.Name) && Value == other.Value;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TestEntity) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Id;
                    hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ Value.GetHashCode();
                    return hashCode;
                }
            }
        }

        private static SqlConnection GetConnection()
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True");
            connection.Open();
            return connection;
        }

        [Fact]
        public void ShouldUseTvpInQuery()
        {
            // Given
            var expected = new[]
                           {
                               new TestEntity {Id = 1, Name = "First", Value = 1},
                               new TestEntity {Id = 2, Value = 2},
                               new TestEntity {Id = 2, Name = "Third"}
                           };
            // When
            TestEntity[] actual;
            using (var connection = GetConnection())
            {
                connection.Execute(@"
if type_id (N'[dbo].[TestList]') is null
	create type [dbo].[TestList] as table(
	    [Id] [int] not null,
        [Name] [nvarchar](max) null,
        [Value] [int] null)");

                actual = connection.Query<TestEntity>(@"
select * from @Param",
                    new
                    {
                        Param = TvpParameter.Create("TestList",
                            expected.Select(x =>
                                new
                                {
                                    x.Id,
                                    x.Name,
                                    x.Value
                                }))
                    })
                    .ToArray();
            }

            // Then
            Assert.Equal(expected, actual);
        }
    }
}