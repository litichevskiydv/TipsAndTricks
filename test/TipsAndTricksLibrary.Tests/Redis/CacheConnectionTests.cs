namespace TipsAndTricksLibrary.Tests.Redis
{
    using System;
    using Xunit;
    public class CacheConnectionTests : IClassFixture<CacheConnectionTestsFixture>
    {
        public class Description
        {
            public int Id { get; set; }
            public string Name { get; set; }

            protected bool Equals(Description other)
            {
                return Id == other.Id && string.Equals(Name, other.Name);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Description) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Id * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                }
            }
        }

        private readonly CacheConnectionTestsFixture _fixture;

        public CacheConnectionTests(CacheConnectionTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldSetKeySimpleValue()
        {
            // Given
            const int expectedValue = 1;

            // When
            var connection = _fixture.ConnectionsFactory.Create();
            connection.AddValue(_fixture.Key, expectedValue);
            var actualValue = connection.GetValue<int>(_fixture.Key);

            // Then
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ShouldSetKeyComplexValue()
        {
            // Given
            var expectedValue = new Description {Id = 1, Name = "First"};

            // When
            var connection = _fixture.ConnectionsFactory.Create();
            connection.AddValue(_fixture.Key, expectedValue);
            var actualValue = connection.GetValue<Description>(_fixture.Key);

            // Then
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ShouldDeleteKey()
        {
            // When
            var connection = _fixture.ConnectionsFactory.Create();
            connection.AddValue(_fixture.Key, 1);
            connection.DeleteByKeys(_fixture.Key);
            var actualValue = connection.GetValue<int?>(_fixture.Key);

            // Then
            Assert.Null(actualValue);
        }
    }
}