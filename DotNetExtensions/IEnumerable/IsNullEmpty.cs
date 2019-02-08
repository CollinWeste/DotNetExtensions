using System.Collections.Generic;

namespace DotNetExtensions.IEnumerable
{
    /// <summary>
    /// Contains extension methods for the <c>System.Collections.Generic.IEnumerable&lt;T&gt;</c> interface
    /// </summary>
    /// <see cref="System.Collections.Generic.IEnumerable{T}"/>
    public static partial class IEnumerableExtensions
    {
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
    }
}
