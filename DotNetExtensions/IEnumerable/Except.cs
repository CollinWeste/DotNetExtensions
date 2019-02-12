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
        /// Returns an enumerable without the specified item.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="item">The item to exclude.</param>
        /// <returns>The enumerable without the specified item.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T item)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }

            return source.Except(new[] { item });
        }
    }
}
