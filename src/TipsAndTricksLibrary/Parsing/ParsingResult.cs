namespace TipsAndTricksLibrary.Parsing
{
    public struct ParsingResult<T> where T : struct
    {
        public ParsingResult(T? value, bool? sourceHasValue = null)
        {
            Value = value;
            SourceHasValue = sourceHasValue ?? value.HasValue;
        }

        public bool SourceHasValue { get; }
        public T? Value { get; }

        public bool Equals(ParsingResult<T> other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ParsingResult<T> && Equals((ParsingResult<T>) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator ParsingResult<T>(T? value)
        {
            return new ParsingResult<T>(value);
        }

        public static implicit operator T?(ParsingResult<T> value)
        {
            return value.Value;
        }
    }
}