namespace TipsAndTricksLibrary.Tests.Parsing
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using TipsAndTricksLibrary.Parsing;
    using Xunit;

    public class ParsingResultTests
    {
        [UsedImplicitly]
        public static readonly IEnumerable<object[]> EqualsTestsData;
        [UsedImplicitly]
        public static readonly IEnumerable<object[]> ConversionTestsData;

        static ParsingResultTests()
        {
            EqualsTestsData = new[]
                              {
                                  new object[] {new ParsingResult<int>(1, true), new ParsingResult<int>(1, false), true},
                                  new object[] {new ParsingResult<int>(1, true), new ParsingResult<int>(1, true), true},
                                  new object[] {new ParsingResult<int>(2, true), new ParsingResult<int>(1, true), false},
                                  new object[] {new ParsingResult<int>(2, true), new ParsingResult<int>(null, true), false},
                                  new object[] {new ParsingResult<int>(null, true), new ParsingResult<int>(null, true), true},
                                  new object[] {new ParsingResult<int>(null, true), new ParsingResult<int>(null, false), true},
                              };
            ConversionTestsData = new[]
                                  {
                                      new object[] {new ParsingResult<int>(5)},
                                      new object[] {new ParsingResult<int>(null)}
                                  };
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(null, true)]
        [InlineData(5, false)]
        [InlineData(5, true)]
        public void ShouldConstructParsingResult(int? value, bool sourceHasValue)
        {
            // When
            var a = new ParsingResult<int>(value, sourceHasValue);

            // Then
            Assert.Equal(value, a.Value);
            Assert.Equal(sourceHasValue, a.SourceHasValue);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(5, true)]
        public void ShouldDetermineSourceHasValueWhenItIsNotPassed(int? value, bool expectedResult)
        {
            // Given
            var a = new ParsingResult<int>(value);

            // When
            var actualResult = a.SourceHasValue;

            // Then
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData(5, "5")]
        public void ShouldGenerateStringRepresentation(int? value, string expectedResult)
        {
            // Given
            var a = new ParsingResult<int>(value);

            // When
            var actualResult = a.ToString();

            // Then
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [MemberData(nameof(EqualsTestsData))]
        public void ShouldCompareTwoParsingResults(ParsingResult<int> a, ParsingResult<int> b, bool expectedResult)
        {
            Assert.Equal(expectedResult, a.Equals(b));
        }

        [Theory]
        [InlineData(5)]
        [InlineData(null)]
        public void ShouldConvertValueToParsingResults(int? value)
        {
            // When
            ParsingResult<int> a = value;

            // Then
            Assert.Equal(value, a.Value);
        }

        [Theory]
        [MemberData(nameof(ConversionTestsData))]
        public void ShouldParsingResultsToNullableValue(ParsingResult<int> value)
        {
            // When
            int? a = value;

            // Then
            Assert.Equal(value.Value, a);
        }
    }
}