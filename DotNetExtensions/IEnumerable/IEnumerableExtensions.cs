using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions.IEnumerable
{
    /// <summary>
    /// Contains extension methods for the <c>System.Collections.Generic.IEnumerable&lt;T&gt;</c> interface
    /// </summary>
    /// <see cref="System.Collections.Generic.IEnumerable{T}"/>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Compares two sequences by comparing the <see cref="IComparable{T}"/> 
        /// elements by using the default comparer for their type.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="compareTo">The enumerable to compare against.</param>
        /// <param name="comparer">(optional) Comparer function for individual elements.</param>
        /// <returns>
        /// Zero if the sequence contents are identical;
        /// Less than zero if <paramref name="source"/> precedes <paramref name="compareTo"/>.
        /// Greater than zero if <paramref name="source"/> follows <paramref name="compareTo"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="compareTo"/> is null.
        /// </exception>
        public static int Compare<T>(this IEnumerable<T> source, IEnumerable<T> compareTo, Func<T, T, int> comparer = null) where T : IComparable
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            if (compareTo == null) { throw new ArgumentNullException(nameof(compareTo)); }

            comparer = comparer ??
                ((a, b) =>
                {
                    if (a.CompareTo(default(T)) == 0 && b.CompareTo(default(T)) == 0) { return 0; }
                    if (a.CompareTo(default(T)) == 0) { return -1; }
                    if (b.CompareTo(default(T)) == 0) { return 1; }

                    return a.CompareTo(b);
                });

            var i = 0;

            while (true)
            {
                var sourceElement = source.ElementAtOrDefault(i);
                var compareElement = compareTo.ElementAtOrDefault(i);

                if ((sourceElement?.CompareTo(default(T)) ?? 0) == 0 &&
                    (compareElement?.CompareTo(default(T)) ?? 0) == 0)
                {
                    return 0;
                }
                if ((sourceElement?.CompareTo(default(T)) ?? 0) == 0)
                {
                    return -1;
                }
                if ((compareElement?.CompareTo(default(T)) ?? 0) == 0)
                {
                    return 1;
                }

                var test = comparer(sourceElement, compareElement);
                if (test != 0)
                {
                    return test;
                }

                i++;
            }
        }

        /// <summary>
        /// Performs the <paramref name="action"/> on each item in the <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="source">The source over which to enumerate.</param>
        /// <param name="action">The action to perform.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="action"/> is null.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            if (action == null) { throw new ArgumentNullException(nameof(action)); }

            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Determines whether the enumerable is empty.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="source">The source >enumerable.</param>
        /// <returns>True if the enumerable is empty; else false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }

            return !source.Any();
        }

        /// <summary>
        /// Determines whether the enumerable is null or empty.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <returns>True if the collection is null or empty; else false.</returns>
        public static bool IsNullEmpty<T>(this IEnumerable<T> source)
        {
            return source?.IsEmpty() ?? true;
        }

        /// <summary>
        /// Removes the specified number of elements from the end of the enumerable.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <returns>
        /// The enumerable with <paramref name="count"/> elements 
        /// removed from the end. If <paramref name="count"/> is larger
        /// than the number of elements in <paramref name="source"/>, an empty
        /// enumerable is returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is less than zero.</exception>
        public static IEnumerable<T> RemoveFromEnd<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            if (count < 0) { throw new ArgumentOutOfRangeException(nameof(count)); }

            var newLength = source.Count() - count;
            if (newLength < 0) { newLength = 0; }

            return source.Take(newLength);
        }
    }
}
