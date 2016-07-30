namespace TipsAndTricksLibrary.Tests.Extensions
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using TipsAndTricksLibrary.Extensions;
    using Xunit;

    public class EnumerableExtensionsTests
    {
        [UsedImplicitly]
        public static IEnumerable<object[]> SequencesComparsionTestsData;

        static EnumerableExtensionsTests()
        {
            SequencesComparsionTestsData = new[]
                                           {
                                               new object[] {new[] {1, 2}, new[] {1, 2}, true},
                                               new object[] {new[] {1, 2}, new[] {1}, false},
                                               new object[] {new[] {1, 2}, null, false},
                                               new object[] {null, new[] {1, 2}, false},
                                               new object[] {null, null, true}
                                           };
        }

        [Theory]
        [MemberData(nameof(SequencesComparsionTestsData))]
        public void ShouldCompareTwoSequences(int[] a, int[] b, bool comparsionResult)
        {
            Assert.Equal(comparsionResult, a.EqualsByElements(b));
        }
    }
}