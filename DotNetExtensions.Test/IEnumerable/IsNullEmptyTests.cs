using DotNetExtensions.IEnumerable;
using NUnit.Framework;
using System.Collections.Generic;

namespace DotNetExtensions.Test.IEnumerable
{
    [TestFixture]
    internal class IsNullEmptyTests
    {
        [Test]
        public void IsNullEmpty_NullSource_IsNullEmpty()
        {
            // Arrange
            IEnumerable<object> uut = null;

            // Act - Assert
            Assert.That(uut.IsNullEmpty(), Is.True);
        }

        [Test]
        public void IsNullEmpty_EmptySource_IsNullEmpty()
        {
            // Arrange
            IEnumerable<object> uut = new List<object>();

            // Act - Assert
            Assert.That(uut.IsNullEmpty(), Is.True);
        }

        [Test]
        public void IsNullEmpty_NonEmptySource_IsNotNullEmpty()
        {
            // Arrange
            IEnumerable<object> uut = new List<object>()
            {
                "item"
            };

            // Act - Assert
            Assert.That(uut.IsNullEmpty(), Is.False);
        }
    }
}
