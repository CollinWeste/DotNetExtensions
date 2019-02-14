using DotNetExtensions.IEnumerable;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions.Test.IEnumerable
{
    [TestFixture]
    internal class ExceptTests
    {
        [Test]
        public void Except_NullSource_ThrowsException()
        {
            // Arrange
            IEnumerable<object> source = null;
            object item = new object();

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => source.Except(item));
            Assert.Throws<ArgumentNullException>(() => source.Except(null));
        }

        [Test]
        public void Except_NullItem_DoesNotThrow()
        {
            // Arrange
            IEnumerable<object> source = new List<object>();
            object item = null;

            // Act - Assert
            Assert.DoesNotThrow(() => source.Except(item));
        }

        [Test]
        public void Except_WithoutItem_ReturnsOriginalSet()
        {
            // Arrange
            IEnumerable<object> source = new List<object>()
            {
                new object(),
                new object(),
                new object()
            };
            object item = new object();

            var expected = source.ToList();

            Assume.That(source, Is.EqualTo(expected));
            Assume.That(source, Is.Not.SameAs(expected));
            Assume.That(source.Contains(item), Is.False);

            // Act
            var result = source.Except(item);

            // Result
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result, Is.Not.SameAs(expected));
        }

        [Test]
        public void Except_WithItemOnce_ReturnsSetWithoutItem()
        {
            // Arrange
            object item = new object();
            IEnumerable<object> source = new List<object>()
            {
                new object(),
                item,
                new object()
            };
            
            Assume.That(source.Contains(item), Is.True);

            // Act
            var result = source.Except(item);

            // Assert
            Assert.That(result.Contains(item), Is.False);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Except_WithItemMultipleTimes_ReturnsSetWithoutItem()
        {
            // Arrange
            object item = new object();
            IEnumerable<object> source = new List<object>()
            {
                item,
                item,
                new object(),
                item,
                new object(),
                item
            };

            Assume.That(source.Contains(item), Is.True);
            Assume.That(source.Count(x => x == item), Is.EqualTo(4));
            Assume.That(source.Count(), Is.EqualTo(6));

            // Act
            var result = source.Except(item);

            // Assert
            Assert.That(result.Contains(item), Is.False);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Except_WithNullItem_RemovesAllNullEntries()
        {
            // Arrange
            object item = null;
            IEnumerable<object> source = new List<object>()
            {
                item,
                item,
                new object(),
                item,
                new object(),
                item
            };

            Assume.That(source.Contains(item), Is.True);
            Assume.That(source.Count(x => x == item), Is.EqualTo(4));
            Assume.That(source.Count(), Is.EqualTo(6));

            // Act
            var result = source.Except(item);

            // Assert
            Assert.That(result.Contains(item), Is.False);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
