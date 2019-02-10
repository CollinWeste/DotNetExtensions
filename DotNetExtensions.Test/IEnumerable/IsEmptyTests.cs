using DotNetExtensions.IEnumerable;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DotNetExtensions.Test.IEnumerable
{
    internal class IsEmptyTests
    {
        [Test]
        public void IsEmpty_NullSource_ThrowsException()
        {
            // Arrange
            IEnumerable<object> uut = null;

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => uut.IsEmpty());
        }

        [Test]
        public void IsEmpty_ValidArgs_DoesNotThrow()
        {
            // Arrange
            IEnumerable<object> uut = new List<object>();

            // Act - Assert
            Assert.DoesNotThrow(() => uut.IsEmpty());
        }

        [Test]
        public void IsEmpty_Empty_IsEmpty()
        {
            // Arrange
            IEnumerable<object> uut = new List<object>();

            // Act - Assert
            Assert.That(uut.IsEmpty(), Is.True);
        }

        [Test]
        public void IsEmpty_NotEmpty_IsNotEmpty()
        {
            // Arrange
            IEnumerable<object> uut = new List<object>()
            {
                "item"
            };

            // Act - Assert
            Assert.That(uut.IsEmpty(), Is.False);
        }
    }
}
