using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions.IEnumerable
{
    /// <summary>
    /// Contains extension methods for the <c>System.Collections.Generic.IEnumerable&lt;T&gt;</c> interface
    /// </summary>
    /// <see cref="System.Collections.Generic.IEnumerable{T}"/>
    public static partial class IEnumerableExtensions
    {
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
