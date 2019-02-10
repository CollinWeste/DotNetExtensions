using DotNetExtensions.Object;
using NUnit.Framework;

namespace DotNetExtensions.Test.Object
{
    [TestFixture]
    internal class AsTests
    {
        [Test]
        public void As_NullSource_DoesNotThrow()
        {
            // Arrange
            object uut = null;

            // Act - Assert
            Assert.DoesNotThrow(() => uut.As<object>());
        }

        [Test]
        public void As_NonNullSource_DoesNotThrow()
        {
            // Arrange
            object uut = new object();

            // Act - Assert
            Assert.DoesNotThrow(() => uut.As<object>());
        }

        [Test]
        public void As_InvalidCast_ReturnsNull()
        {
            // Arrange
            object uut = new object();

            // Act
            var result = uut.As<string>();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void As_InvalidCast_CastsObject()
        {
            // Arrange
            object uut = "";

            // Act
            var result = uut.As<string>();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.SameAs(uut));
        }
    }
}
