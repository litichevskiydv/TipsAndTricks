namespace TipsAndTricksLibrary.Parsing
{
    using System;
    using System.Data;
    using Dapper;

    public class ParsingResultHandler<T> : SqlMapper.TypeHandler<ParsingResult<T>> where T : struct
    {
        public override void SetValue(IDbDataParameter parameter, ParsingResult<T> value)
        {
            SqlMapper.ITypeHandler handler;
            parameter.DbType = SqlMapper.LookupDbType(typeof(T), "n/a", false, out handler);
            parameter.Value = value.Value ?? (object) DBNull.Value;
        }

        public override ParsingResult<T> Parse(object value)
        {
            if (value is T)
                return new ParsingResult<T>((T) value);
            throw new FormatException($"Invalid conversion to {typeof(T?)}");
        }
    }

    public class DateTimeParsingResultHandler : ParsingResultHandler<DateTime>
    {
    }

    public class BoolParsingResultHandler : ParsingResultHandler<bool>
    {
    }

    public class IntParsingResultHandler : ParsingResultHandler<int>
    {
    }

    public class LongParsingResultHandler : ParsingResultHandler<long>
    {
    }

    public class DoubleParsingResultHandler : ParsingResultHandler<double>
    {
    }

    public class DecimalParsingResultHandler : ParsingResultHandler<decimal>
    {
    }
}