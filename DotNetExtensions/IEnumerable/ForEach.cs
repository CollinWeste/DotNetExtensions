using System;
using System.Collections.Generic;

namespace DotNetExtensions.IEnumerable
{
    /// <summary>
    /// Contains extension methods for the <see cref="System.Collections.Generic.IEnumerable{T}"/> interface
    /// </summary>
    /// <see cref="System.Collections.Generic.IEnumerable{T}"/>
    public static partial class IEnumerableExtensions
    {
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
    }
}
