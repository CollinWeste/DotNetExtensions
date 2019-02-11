using System.Collections.Generic;

namespace DotNetExtensions.Csv
{
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
    }
}
