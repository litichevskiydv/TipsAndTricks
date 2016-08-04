namespace TipsAndTricksLibrary.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ArrayExtensions
    {
        public static int? UpperBound<TSource>(this IReadOnlyList<TSource> source, TSource value, int? startIndex = null, int? endIndex = null,
            IComparer<TSource> itemsComparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source.Count == 0)
                throw new ArgumentException(nameof(source));

            var first = startIndex ?? 0;
            if (first < 0 || first >= source.Count)
                throw new ArgumentException(nameof(startIndex));

            var last = (endIndex ?? source.Count - 1) + 1;
            if (first > last || last > source.Count)
                throw new ArgumentException(nameof(endIndex));

            var comparer = itemsComparer ?? Comparer<TSource>.Default;

            var count = last - first;
            while (count > 0)
            {
                var current = first;
                var step = count / 2;
                current += step;

                if (comparer.Compare(value, source[current]) >= 0)
                {
                    first = current + 1;
                    count -= step + 1;
                }
                else
                    count = step;
            }

            return first == last ? (int?)null : first;
        }
    }
}