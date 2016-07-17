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

        public static ParsingResult<bool> ParseBool(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ParsingResult<bool>(null, false);

            bool parsedBool;
            return bool.TryParse(input, out parsedBool)
                ? new ParsingResult<bool>(parsedBool, true)
                : new ParsingResult<bool>(null, true);
        }

        public static ParsingResult<int> ParseInt(this string input, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ParsingResult<int>(null, false);

            int parsedInt;
            return int.TryParse(input, NumberStyles.Any, formatProvider ?? CultureInfo.InvariantCulture, out parsedInt)
                ? new ParsingResult<int>(parsedInt, true)
                : new ParsingResult<int>(null, true);
        }

        public static ParsingResult<long> ParseLong(this string input, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ParsingResult<long>(null, false);

            long parsedLong;
            return long.TryParse(input, NumberStyles.Any, formatProvider ?? CultureInfo.InvariantCulture, out parsedLong)
                ? new ParsingResult<long>(parsedLong, true)
                : new ParsingResult<long>(null, true);
        }

        public static ParsingResult<double> ParseDouble(this string input, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ParsingResult<double>(null, false);

            double parsedDouble;
            return double.TryParse(input, NumberStyles.Any, formatProvider ?? CultureInfo.InvariantCulture, out parsedDouble)
                ? new ParsingResult<double>(parsedDouble, true)
                : new ParsingResult<double>(null, true);
        }

        public static ParsingResult<decimal> ParseDecimal(this string input, IFormatProvider formatProvider = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ParsingResult<decimal>(null, false);

            decimal parsedDecimal;
            return decimal.TryParse(input, NumberStyles.Any, formatProvider ?? CultureInfo.InvariantCulture, out parsedDecimal)
                ? new ParsingResult<decimal>(parsedDecimal, true)
                : new ParsingResult<decimal>(null, true);
        }
    }
}