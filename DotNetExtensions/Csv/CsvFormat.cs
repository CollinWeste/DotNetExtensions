
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
    /// retain csv structure.
    /// </summary>
    Normal,

    /// <summary>
    /// Entries are never quoted even when whitespace is present.
    /// Entries cannot contain commas.
    /// </summary>
    Strict
  }
}
