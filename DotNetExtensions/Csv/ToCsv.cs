using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetExtensions.Csv
{
    /// <summary>
    /// Contains Csv extension methods
    /// </summary>
    /// <seealso cref="CsvFormat"/>
    public static partial class CsvExtensions
    {
        /// <summary>
        /// Converts the <paramref name="source"/> enuemrable to a Csv string.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="source">The source enumerable to join.</param>
        /// <param name="format">(optional) Csv format to use during join.</param>
        /// <param name="selector">
        /// (optional) Selector to convert entries to string equivalents.
        /// Defaults to <see cref="System.Object.ToString"/> if none specified.
        /// </param>
        /// <returns>Csv string of the joined entries.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public static string ToCsv<T>(this IEnumerable<T> source, CsvFormat format = CsvFormat.Normal, Func<T, string> selector = null)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }

            selector = selector ?? ((item) => item?.ToString());
            var entries = source.Select(selector)
                .Select(entry =>
                {
                    return format == CsvFormat.Normal
                      ? Regex.IsMatch(entry, "[,]")
                        ? $"\"{entry}\""
                        : entry
                      : format == CsvFormat.Strict
                        ? Regex.IsMatch(entry, "[,]")
                          ? throw new InvalidOperationException($"Strict mode does not support nested commas.\r\nEntry: {entry}")
                          : entry
                        : null;
                });

            return string.Join(",", entries);
        }
    }
}
