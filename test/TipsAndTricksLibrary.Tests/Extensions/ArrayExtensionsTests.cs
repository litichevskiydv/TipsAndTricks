namespace TipsAndTricksLibrary.Tests.Extensions
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using TipsAndTricksLibrary.Extensions;
    using Xunit;

    public class ArrayExtensionsTests
    {
        [UsedImplicitly]
        public static IEnumerable<object[]> UpperBoundTestsData;
        [UsedImplicitly]
        public static IEnumerable<object[]> LowerBoundTestsData;

        static ArrayExtensionsTests()
        {
            UpperBoundTestsData = new[]
                                  {
                                      new object[] {new[] {1, 2, 3}, 2, null, null, 2},
                                      new object[] {new[] {1, 2, 3}, 0, null, null, 0},
                                      new object[] {new[] {1, 2, 5}, 5, null, null, null},
                                      new object[] {new[] {1, 2, 3}, 2, null, 1, null},
                                      new object[] {new[] {1, 2, 3}, 0, 1, null, 1},
                                  };
            LowerBoundTestsData = new[]
                                  {
                                      new object[] {new[] {1, 2, 3}, 2, null, null, 1},
                                      new object[] {new[] {1, 2, 3}, 0, null, null, 0},
                                      new object[] {new[] {1, 2, 5}, 5, null, null, 2},
                                      new object[] {new[] {1, 2, 5}, 6, null, null, null},
                                      new object[] {new[] {1, 2, 3}, 2, null, 1, 1},
                                      new object[] {new[] {1, 2, 3}, 3, null, 1, null},
                                      new object[] {new[] {1, 2, 3}, 0, 1, null, 1},
                                  };
        }

        [Theory]
        [MemberData(nameof(UpperBoundTestsData))]
        public void ShouldFindUpperBound(int[] source, int value, int? startIndex, int? endIndex, int? expectedResult)
        {
            Assert.Equal(expectedResult, source.UpperBound(value, startIndex: startIndex, endIndex: endIndex));
        }

        [Theory]
        [MemberData(nameof(LowerBoundTestsData))]
        public void ShouldFindLowerBound(int[] source, int value, int? startIndex, int? endIndex, int? expectedResult)
        {
            Assert.Equal(expectedResult, source.LowerBound(value, startIndex: startIndex, endIndex: endIndex));
        }
    }
}