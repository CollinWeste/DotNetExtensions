namespace DotNetExtensions.Csv
{
    /// <summary>
    /// Enum for Csv operations, used with DotNetExtensions.Csv.CsvExtensions
    /// </summary>
    /// <see cref="CsvExtensions"/>
    public enum CsvFormat
    {
        /// <summary>
        /// Entries are quoted if they contain spaces or commas to 
        /// retain csv structure. Does not support nested quotes.
        /// </summary>
        /// <remarks>
        /// When parsing, will remove quotes from quoted strings.
        /// Quotes are added back when the enumerable is converted back.
        /// </remarks>
        Normal,

        /// <summary>
        /// Entries are never quoted even when whitespace is present.
        /// Entries cannot contain commas.
        /// </summary>
        Strict
    }
}
