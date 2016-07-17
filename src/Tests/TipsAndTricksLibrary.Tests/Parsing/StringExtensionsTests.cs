namespace TipsAndTricksLibrary.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using TipsAndTricksLibrary.Parsing;
    using Xunit;

    public class StringExtensionsTests
    {
        [UsedImplicitly]
        public static IEnumerable<object[]> ParseDateTimeTestsData;
        [UsedImplicitly]
        public static IEnumerable<object[]> ParseBoolTestsData;
        [UsedImplicitly]
        public static IEnumerable<object[]> ParseIntTestsData;
        [UsedImplicitly]
        public static IEnumerable<object[]> ParseLongTestsData;

        static StringExtensionsTests()
        {
            ParseDateTimeTestsData = new[]
                                     {
                                         new object[]
                                         {
                                             " 17.07.2016 15:04 ", null,
                                             new ParsingResult<DateTime>(new DateTime(2016, 07, 17, 15, 04, 00), true)
                                         },
                                         new object[] {null, null, new ParsingResult<DateTime>(null, false)},
                                         new object[] {"  ", null, new ParsingResult<DateTime>(null, false)},
                                         new object[] {"17.07.2016", null, new ParsingResult<DateTime>(null, true)},
                                         new object[] {"17.07.2016 15:04", "ddMMyyyy", new ParsingResult<DateTime>(null, true)},
                                         new object[]
                                         {
                                             "20160717", "yyyyMMdd", new ParsingResult<DateTime>(new DateTime(2016, 07, 17), true)
                                         }
                                     };
            ParseBoolTestsData = new[]
                                 {
                                     new object[] {"true", new ParsingResult<bool>(true, true)},
                                     new object[] {"false", new ParsingResult<bool>(false, true)},
                                     new object[] {"d", new ParsingResult<bool>(null, true)},
                                     new object[] {"", new ParsingResult<bool>(null, false)},
                                     new object[] {null, new ParsingResult<bool>(null, false)}
                                 };
            ParseIntTestsData = new[]
                                 {
                                     new object[] {"1", new ParsingResult<int>(1, true)},
                                     new object[] {"d", new ParsingResult<int>(null, true)},
                                     new object[] {"", new ParsingResult<int>(null, false)},
                                     new object[] {null, new ParsingResult<int>(null, false)}
                                 };
            ParseLongTestsData = new[]
                                 {
                                     new object[] {"1", new ParsingResult<long>(1, true)},
                                     new object[] {"d", new ParsingResult<long>(null, true)},
                                     new object[] {"", new ParsingResult<long>(null, false)},
                                     new object[] {null, new ParsingResult<long>(null, false)}
                                 };
        }

        [Theory]
        [MemberData(nameof(ParseDateTimeTestsData))]
        public void ShouldParseDateTime(string input, string format, ParsingResult<DateTime> expectedResult)
        {
            // When
            var actualResult = input.ParseDateTime(format);

            //Then
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.SourceHasValue, actualResult.SourceHasValue);
        }

        [Theory]
        [MemberData(nameof(ParseBoolTestsData))]
        public void ShouldParseBool(string input, ParsingResult<bool> expectedResult)
        {
            // When
            var actualResult = input.ParseBool();

            // Then
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.SourceHasValue, actualResult.SourceHasValue);
        }

        [Theory]
        [MemberData(nameof(ParseIntTestsData))]
        public void ShouldParseInt(string input, ParsingResult<int> expectedResult)
        {
            // When
            var actualResult = input.ParseInt();

            // Then
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.SourceHasValue, actualResult.SourceHasValue);
        }

        [Theory]
        [MemberData(nameof(ParseLongTestsData))]
        public void ShouldParseLong(string input, ParsingResult<long> expectedResult)
        {
            // When
            var actualResult = input.ParseLong();

            // Then
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.SourceHasValue, actualResult.SourceHasValue);
        }
    }
}