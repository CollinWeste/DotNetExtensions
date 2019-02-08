using DotNetExtensions.IEnumerable;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions.Test.IEnumerable
{
    [TestFixture]
    internal class RemoveFromEndTests
    {
        [Test]
        public void RemoveFromEnd_NullSource_ThrowsException()
        {
            // Arrange
            IEnumerable<object> source = null;
            int count = 0;

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => source.RemoveFromEnd(count));
        }

        [Test]
        public void RemoveFromEnd_NegativeCount_ThrowsException()
        {
            // Arrange
            IEnumerable<object> source = new List<object>();
            int count = -1;

            // Act - Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => source.RemoveFromEnd(count));
        }

        [Test]
        public void RemoveFromEnd_ValidArgs_DoesNotThrow()
        {
            // Arrange
            IEnumerable<object> source = new List<object>();
            int count = 0;

            // Act - Assert
            Assert.DoesNotThrow(() => source.RemoveFromEnd(count));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(4)]
        public void RemoveFromEnd_CountLessThanEqualToLength_RemovesFromEnd(int toRemove)
        {
            // Arrange
            IEnumerable<object> source = new List<object>()
            {
              "1",
              "2",
              "3",
              "4"
            };

            var expected = source.Reverse().Skip(toRemove).Reverse();
            var expectedLength = expected.Count();

            Assume.That(expectedLength, Is.Not.Negative);

            // Act
            source = source.RemoveFromEnd(toRemove);

            // Assert
            Assert.That(source.Count(), Is.EqualTo(expectedLength));

            for (var i = 0; i < source.Count(); i++)
            {
                Assert.That(source.ElementAt(i), Is.EqualTo(expected.ElementAt(i)));
            }
        }

        [TestCase(5)]
        [TestCase(15)]
        [TestCase(150)]
        public void RemoveFromEnd_CountGreaterThanLength_RemovesAll(int toRemove)
        {
            // Arrange
            IEnumerable<object> source = new List<object>()
            {
              "1",
              "2",
              "3",
              "4"
            };

            Assume.That(source.Count(), Is.LessThan(toRemove));

            // Act
            source = source.RemoveFromEnd(toRemove);

            // Assert
            Assert.That(source, Is.Empty);
        }
    }
}
