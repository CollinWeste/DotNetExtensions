using DotNetExtensions.Object;
using NUnit.Framework;
using System;

namespace DotNetExtensions.Test.Object
{
    [TestFixture]
    internal class ConvertToTests
    {
        [Test]
        public void ConvertTo_NullSource_ThrowsException()
        {
            // Arrange
            object source = null;

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => source.ConvertTo<bool>());
            Assert.Throws<ArgumentNullException>(() => source.ConvertTo<int>());
            Assert.Throws<ArgumentNullException>(() => source.ConvertTo<float>());
            Assert.Throws<ArgumentNullException>(() => source.ConvertTo<double>());
            Assert.Throws<ArgumentNullException>(() => source.ConvertTo<object>());
            Assert.Throws<ArgumentNullException>(() => source.ConvertTo<string>());
        }

        [Test]
        public void ConvertTo_InvalidCast_ThrowsException()
        {
            // Arrange
            object source = new object();

            // Act - Assert
            Assert.Throws<InvalidCastException>(() => source.ConvertTo<bool>());
            Assert.Throws<InvalidCastException>(() => source.ConvertTo<int>());
            Assert.Throws<InvalidCastException>(() => source.ConvertTo<float>());
            Assert.Throws<InvalidCastException>(() => source.ConvertTo<double>());
        }

        // Convert to bool
        [TestCase(0, typeof(bool), false)]
        [TestCase(1, typeof(bool), true)]
        [TestCase(1.1f, typeof(bool), true)]
        [TestCase(1.1, typeof(bool), true)]
        [TestCase("False", typeof(bool), false)]
        [TestCase("True", typeof(bool), true)]
        // Convert to Int
        [TestCase(1.1f, typeof(int), 1)]
        [TestCase(1.1, typeof(int), 1)]
        [TestCase(1.5f, typeof(int), 2)]
        [TestCase(1.5, typeof(int), 2)]
        // Convert to Float
        [TestCase(1, typeof(float), 1f)]
        [TestCase(1.1, typeof(float), 1.1f)]
        // Convert to Double
        [TestCase(1, typeof(double), 1.0)]
        [TestCase(1.1f, typeof(double), 1.1, Ignore = "The up-cast creates a slight rounding error.")]
        // Convert to String
        [TestCase(0, typeof(string), "0")]
        [TestCase(1.1f, typeof(string), "1.1")]
        [TestCase(1.1, typeof(string), "1.1")]
        public void ConvertTo_ValidCast_ConvertsCorrectly(object source, Type targetType, object expected)
        {
            // Act
            object result = null;

            if (targetType == typeof(bool))
            {
                result = source.TryConvertTo<bool>();
            }
            else if (targetType == typeof(int))
            {
                result = source.TryConvertTo<int>();
            }
            else if (targetType == typeof(float))
            {
                result = source.TryConvertTo<float>();
            }
            else if (targetType == typeof(double))
            {
                result = source.TryConvertTo<double>();
            }
            else if (targetType == typeof(object))
            {
                // This is a fairly useless cast, but verify the object didn't change
                result = source.TryConvertTo<object>();
            }
            else if (targetType == typeof(string))
            {
                result = source.TryConvertTo<string>();
            }

            // Assert
            if (result != null)
            {
                Assert.That(result.GetType(), Is.EqualTo(expected.GetType()));
            }

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
