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
    }
}
