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
                                         },
                                     };
        }

        [Theory]
        [MemberData(nameof(ParseDateTimeTestsData))]
        public void ShouldParseDateTime(string input, string format, ParsingResult<DateTime> expectedResult)
        {
            // When
            var actualResult = input.ParseDateTime(format);

            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedResult.SourceHasValue, actualResult.SourceHasValue);
        }
    }
}