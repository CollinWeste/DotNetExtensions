
using DotNetExtensions.Csv;
using NUnit.Framework;
using System;

namespace DotNetExtensions.Test.Csv
{
  [TestFixture]
  public class CsvExtensionsTests
  {
    #region ParseCsv

    [Test]
    public void ParseCsv_NullSource_ThrowsException()
    {
      // Arrange
      string uut = null;

      // Act - Assert
      Assert.Multiple(() =>
      {
        Assert.Throws<ArgumentNullException>(() => uut.ParseCsv(CsvFormat.Normal, null));
        Assert.Throws<ArgumentNullException>(() => uut.ParseCsv(CsvFormat.Strict, null));

        Assert.Throws<ArgumentNullException>(() => uut.ParseCsv(CsvFormat.Normal, selector: x => x));
        Assert.Throws<ArgumentNullException>(() => uut.ParseCsv(CsvFormat.Strict, selector: x => x));
      });
    }

    [TestCase("")]
    [TestCase("asdf")]
    [TestCase("test,string")]
    public void ParseCsv_NonNullSource_DoesNotThrow(string testString)
    {
      // Arrange
      string uut = testString;

      // Act - Assert
      Assert.Multiple(() =>
      {
        Assert.DoesNotThrow(() => uut.ParseCsv(CsvFormat.Normal, null));
        Assert.DoesNotThrow(() => uut.ParseCsv(CsvFormat.Strict, null));

        Assert.DoesNotThrow(() => uut.ParseCsv(CsvFormat.Normal, selector: x => x));
        Assert.DoesNotThrow(() => uut.ParseCsv(CsvFormat.Strict, selector: x => x));
      });
    }

    #endregion
  }
}
