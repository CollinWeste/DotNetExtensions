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
        /// Parses the <paramref name="source"/> string into an enumerable of <see cref="System.String"/>.
        /// </summary>
        /// <param name="source">The source string to parse.</param>
        /// <param name="format">(optional) Csv format to use during parsing.</param>
        /// <param name="selector">(optional) Selector to alter the <see cref="System.String"/> entries.</param>
        /// <returns>The enumerable of <see cref="System.String"/> entries.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public static IEnumerable<string> ParseCsv(this string source, CsvFormat format = CsvFormat.Normal, Func<string, string> selector = null)
        {
            return source.ParseCsv<string>(format, selector ?? (entry => entry));
        }

        /// <summary>
        /// Parses the <paramref name="source"/> string into an enumerable of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="source">The source string to parse.</param>
        /// <param name="format">(optional) Csv format to use during parsing.</param>
        /// <param name="selector">
        /// (optional) Selector to convert <see cref="System.String"/>
        /// entries to <typeparamref name="T"/> objects.
        /// Defaults to <see cref="System.Object.ToString"/> if none specified.
        /// </param>
        /// <returns>The enumerable of <typeparamref name="T"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        public static IEnumerable<T> ParseCsv<T>(this string source, CsvFormat format = CsvFormat.Normal, Func<string, T> selector = null)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            if (string.IsNullOrEmpty(source)) { return new T[0]; }

            selector = selector ?? ((entry) => (T)Convert.ChangeType(entry, typeof(T)));
            var splits = Regex.Matches(source, RegexByFormat[format])
                .OfType<Match>()
                .Select(x => x.Groups.OfType<Group>().ElementAt(2).Value);

            // For some reason, Regex.Matches doesn't catch the double-comma at the beginning
            // of strings. For this case, we need to explicitly add one.
            if (source.StartsWith(",,"))
            {
                splits = new[] { string.Empty }.Concat(splits);
            }

            return splits.Select(selector);
        }
    }
}
