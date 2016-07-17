namespace TipsAndTricksLibrary.Parsing
{
    using System;
    using System.Globalization;

    public static class StringExtensions
    {
        public static ParsingResult<DateTime> ParseDateTime(this string input, string formar = null, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ParsingResult<DateTime>(null, false);

            DateTime parsedDateTime;
            return DateTime.TryParseExact(input,
                formar ?? "dd.MM.yyyy HH:mm",
                formatProvider ?? CultureInfo.InvariantCulture,
                DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite,
                out parsedDateTime)
                ? new ParsingResult<DateTime>(parsedDateTime, true)
                : new ParsingResult<DateTime>(null, true);
        }
    }
}