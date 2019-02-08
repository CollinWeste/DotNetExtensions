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
    }
}
