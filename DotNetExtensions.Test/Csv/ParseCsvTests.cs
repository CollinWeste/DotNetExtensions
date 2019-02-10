using DotNetExtensions.Csv;
using NUnit.Framework;
using System;

namespace DotNetExtensions.Test.Csv
{
    [TestFixture]
    internal class ParseCsvTests
    {
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

        #region CsvFormat Normal

        [Test]
        public void ParseCsvNormal_EmptyString_ReturnsEmptyEnumerable()
        {
            // Arrange
            var uut = string.Empty;

            // Act
            var result = uut.ParseCsv();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [TestCase("test")]
        [TestCase("This is a Test")]
        [TestCase("This is \r\n a Test")]
        public void ParseCsvNormal_SingleEntry_ReturnsOriginalString(string testString)
        {
            // Act
            var result = testString.ParseCsv();

            // Assert
            Assert.That(result, Is.EqualTo(new[] { testString }));
        }
        
        [TestCase("a,b,c", "a", "b", "c")]
        [TestCase("test,this,function", "test", "this", "function")]
        [TestCase("test,,,empty,entries", "test", "", "", "empty", "entries")]
        [TestCase(",,test,leading,entries", "", "", "test", "leading", "entries")]
        [TestCase("test,trailing,entries,,", "test", "trailing", "entries", "", "")]
        [TestCase("test, whitespace\r\n ,entries", "test", " whitespace\r\n ", "entries")]
        public void ParseCsvNormal_NoQuotedEntries_CorrectlySplitsString(string testString, params string[] expected)
        {
            // Act
            var result = testString.ParseCsv();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }


        [TestCase("a,\"b\",c", "a", "b", "c")]
        [TestCase("a,\" b \",c", "a", " b ", "c")]
        [TestCase("\"string,has,commas\",asdf", "string,has,commas", "asdf")]
        [TestCase("\"string has whitespace\",asdf", "string has whitespace", "asdf")]
        [TestCase("\"string, has, both\",asdf", "string, has, both", "asdf")]
        [TestCase("test,\"string,has,commas\"", "test", "string,has,commas")]
        [TestCase("test,\"string has whitespace\"", "test", "string has whitespace")]
        [TestCase("test,\"string, has, both\"", "test", "string, has, both")]
        [TestCase("test,\"string,has,commas\",asdf", "test", "string,has,commas", "asdf")]
        [TestCase("test,\"string has whitespace\",asdf", "test", "string has whitespace", "asdf")]
        [TestCase("test,\"string, has, both\",asdf", "test", "string, has, both", "asdf")]
        public void ParseCsvNormal_QuotedEntries_CorrectlySplitsString(string testString, params string[] expected)
        {
            // Act
            var result = testString.ParseCsv();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion

        #region CsvFormat Strict

        // todo

        #endregion
    }
}
