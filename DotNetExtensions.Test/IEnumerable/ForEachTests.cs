using DotNetExtensions.IEnumerable;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DotNetExtensions.Test.IEnumerable
{
    [TestFixture]
    internal class ForEachTests
    {
        [Test]
        public void ForEach_NullSource_ThrowsException()
        {
            // Arrange
            IEnumerable<object> source = null;
            Action<object> action = (x) => { };

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => source.ForEach(action));
        }

        [Test]
        public void ForEach_NullAction_ThrowsException()
        {
            // Arrange
            IEnumerable<object> source = new List<object>();
            Action<object> action = null;

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => source.ForEach(action));
        }

        [Test]
        public void ForEach_NonNullArgs_DoesNotThrow()
        {
            // Arrange
            IEnumerable<object> source = new List<object>();
            Action<object> action = (x) => { };

            // Act - Assert
            Assert.DoesNotThrow(() => source.ForEach(action));
        }

        [Test]
        public void ForEach_ValidArgs_DoesActionForEveryItem()
        {
            // Arrange
            var itemsFound = new List<string>();
            var source = new List<string>()
            {
                "This",
                "is",
                "a",
                "test"
            };

            Action<string> action = (item) => itemsFound.Add(item);

            Assume.That(itemsFound, Is.Not.EqualTo(source));

            // Act
            source.ForEach(action);

            // Assert
            Assert.That(itemsFound, Is.EqualTo(source));
        }
    }
}
