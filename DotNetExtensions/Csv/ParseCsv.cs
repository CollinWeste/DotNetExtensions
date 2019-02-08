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
        /// Regex to split strings for <see cref="CsvFormat.Normal"/> format.
        /// </summary>
        private const string NormalRegex = "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)";

        /// <summary>
        /// Regex to split strings for <see cref="CsvFormat.Strict"/> format.
        /// </summary>
        private const string StrictRegex = "[,]";

        /// <summary>
        /// Lookup for regex strings in Csv parsing, by <see cref="CsvFormat"/>.
        /// </summary>
        private static readonly Dictionary<CsvFormat, string> RegexByFormat
            = new Dictionary<CsvFormat, string>
            {
                { CsvFormat.Normal, NormalRegex },
                { CsvFormat.Strict, StrictRegex }
            };

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
            selector = selector ?? ((entry) => (T)Convert.ChangeType(entry, typeof(T)));
            var splits = Regex.Split(source, RegexByFormat[format]);

            return splits.Select(selector);
        }
    }
}
