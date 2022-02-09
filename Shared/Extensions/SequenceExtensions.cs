namespace HotelAutomationApp.Shared.Extensions;

public static class SequenceExtensions
{
    /// <summary>
    /// Accepts a sequence by reference, filters after excluding the resulting sequence from the source
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="remain"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> ExcludeAfterFilter<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        out IEnumerable<T> remain)
    {
        var filtered = source.Select(q => new {Element = q, Passed = predicate.Invoke(q)})
            .GroupBy(q => q.Passed)
            .ToDictionary(key => key.Key, value => value.Select(q => q.Element).ToList());

        remain = filtered.ContainsKey(false) ? filtered[false] : new List<T>();

        return filtered.ContainsKey(true) ? filtered[true] : new List<T>();
    }
    
    /// <summary>
        /// Exclude element by finding a match in the second sequence
        /// </summary>
        /// <param name="source"></param>
        /// <param name="comparable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollection<T> ExcludeSameElements<T>(
            this IEnumerable<T> source,
            IEnumerable<T> comparable)
            where T : IEquatable<T> =>
            source.Where(q => !comparable.Any(w => w.Equals(q))).ToList();

        /// <summary>
        /// Exclude element by finding a match in the second sequence
        /// </summary>
        /// <param name="source"></param>
        /// <param name="comparable"></param>
        /// <param name="comparableField"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TComparable"></typeparam>
        /// <returns></returns>
        public static ICollection<T> ExcludeSameElements<T, TComparable>(
            this IEnumerable<T> source,
            IEnumerable<T> comparable,
            Func<T, TComparable> comparableField)
            where TComparable : IEquatable<TComparable> =>
            source.Where(q => !comparable.Any(w => comparableField.Invoke(w).Equals(comparableField.Invoke(q))))
                .ToList();

        /// <summary>
        /// Exclude element by finding a match in the second sequence
        /// </summary>
        /// <param name="source"></param>
        /// <param name="comparable"></param>
        /// <param name="sourceComparableField"></param>
        /// <param name="targetComparableField"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TComparable"></typeparam>
        /// <returns></returns>
        public static ICollection<TFirst> ExcludeSameElements<TFirst, TSecond, TComparable>(
            this IEnumerable<TFirst> source,
            IEnumerable<TSecond> comparable,
            Func<TFirst, TComparable> sourceComparableField,
            Func<TSecond, TComparable> targetComparableField)
            where TComparable : IEquatable<TComparable> =>
            source.Where(q => !comparable.Any(w => targetComparableField.Invoke(w)
                .Equals(sourceComparableField.Invoke(q)))).ToList();
}