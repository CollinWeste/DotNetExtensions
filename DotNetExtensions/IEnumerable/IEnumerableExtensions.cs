
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
  }
}
