namespace TipsAndTricksLibrary.Tests.DbCommands
{
    using System.Data.SqlClient;
    using Dapper;
    using TipsAndTricksLibrary.DbCommands;
    using Xunit;
    using System.Linq;
    using JetBrains.Annotations;

    public class ConnectionExtensionsTests
    {
        private class TestEntity
        {
            public int Id { get; [UsedImplicitly]set; }

            public string Name { get; set; }

            public int Value { get; set; }
        }

        private static SqlConnection GetConnection()
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True");
            connection.Open();
            return connection;
        }

        [Fact]
        public void ShouldPerformBulkInsert()
        {
            // Given
            var entities = new[]
                           {
                               new TestEntity {Name = "First", Value = 1},
                               new TestEntity {Name = "Second", Value = 2},
                               new TestEntity {Name = "Third", Value = 3}
                           };

            // When
            TestEntity[] insertedEntities;
            using (var connection = GetConnection())
            {
                connection.Execute(@"create table #TestEntities (Id int identity(1, 1) not null, Name nvarchar(max) not null, Value int not null)");
                connection.BulkInsert("#TestEntities", entities);
                insertedEntities = connection.Query<TestEntity>("select * from #TestEntities").ToArray();
            }

            // Then
            Assert.Equal(3, insertedEntities.Length);

            Assert.True(insertedEntities[0].Id > 0);
            Assert.Equal(entities[0].Name, insertedEntities[0].Name);
            Assert.Equal(entities[0].Value, insertedEntities[0].Value);

            Assert.True(insertedEntities[1].Id > 0);
            Assert.Equal(entities[1].Name, insertedEntities[1].Name);
            Assert.Equal(entities[1].Value, insertedEntities[1].Value);

            Assert.True(insertedEntities[2].Id > 0);
            Assert.Equal(entities[2].Name, insertedEntities[2].Name);
            Assert.Equal(entities[2].Value, insertedEntities[2].Value);
        }
    }
}