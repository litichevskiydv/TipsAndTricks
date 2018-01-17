namespace TipsAndTricksLibrary.Tests.Testing
{
    using System.Linq;
    using JetBrains.Annotations;
    using KellermanSoftware.CompareNetObjects;
    using TipsAndTricksLibrary.Testing;
    using Xunit;

    public class CompareLogicExtensionsTests
    {
        private class TestEntity
        {
            public int Id { [UsedImplicitly] get; set; }

            public string Name { get; set; }

            public int Value { [UsedImplicitly] get; set; }
        }

        [Fact]
        public void ShouldExcludePropretyFromComparsion()
        {
            // Given
            var a = new TestEntity {Id = 1, Name = "a", Value = 1};
            var b = new TestEntity { Id = 1, Name = "b", Value = 1 };

            // When
            var compareLogic = new CompareLogic()
                .ExcludePropertyFromComparison<TestEntity, string>(x => x.Name);

            // Then
            Assert.True(compareLogic.Compare(a, b).AreEqual);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void ShouldSetupColloctionsOrderChecking(bool ignoreCollectionsOrder, bool hasDifferences)
        {
            // Given
            var first = new[] {1, 2};
            var second = new[] {2, 1};

            // When
            var compareLogic = new CompareLogic()
                .SetupCollectionsOrderChecking(ignoreCollectionsOrder);

            // Then
            Assert.Equal(hasDifferences, compareLogic.Compare(first, second).Differences.Any());
        }
    }
}