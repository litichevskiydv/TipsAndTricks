namespace TipsAndTricksLibrary.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static bool EqualsByElements<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (ReferenceEquals(first, second))
                return true;
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
                return false;
            return first.SequenceEqual(second);
        }
    }
}