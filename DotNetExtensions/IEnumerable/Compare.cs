using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions.IEnumerable
{
    /// <summary>
    /// Contains extension methods for the <see cref="System.Collections.Generic.IEnumerable{T}"/> interface
    /// </summary>
    /// <see cref="System.Collections.Generic.IEnumerable{T}"/>
    public static partial class IEnumerableExtensions
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
    }
}
