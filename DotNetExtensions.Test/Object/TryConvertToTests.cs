using DotNetExtensions.Object;
using NUnit.Framework;
using System;

namespace DotNetExtensions.Test.Object
{
    [TestFixture]
    internal class TryConvertToTests
    {
        [TestCase(typeof(bool), default(bool))]
        [TestCase(typeof(int), default(int))]
        [TestCase(typeof(float), default(float))]
        [TestCase(typeof(double), default(double))]
        [TestCase(typeof(object), default(object))]
        [TestCase(typeof(string), default(string))]
        public void TryConvertTo_NullSource_ReturnsDefault(Type type, object expected)
        {
            // Arrange
            object source = null;

            // Act
            object result = null;

            // I didn't know another way to do this, alas
            if (type == typeof(bool))
            {
                result = source.TryConvertTo<bool>();
            }
            else if (type == typeof(int))
            {
                result = source.TryConvertTo<int>();
            }
            else if (type == typeof(float))
            {
                result = source.TryConvertTo<float>();
            }
            else if (type == typeof(double))
            {
                result = source.TryConvertTo<double>();
            }
            else if (type == typeof(object))
            {
                // This is a fairly useless cast, but verify the object didn't change
                result = source.TryConvertTo<object>();
            }
            else if (type == typeof(string))
            {
                result = source.TryConvertTo<string>();
            }

            // Assert
            Assert.That(result?.GetType() ?? typeof(object), Is.EqualTo(expected?.GetType() ?? typeof(object)));
            Assert.That(result, Is.EqualTo(expected));
        }

        // Convert to Bool -- non-zero numerics convert -> true
        [TestCase("Test string", typeof(bool), default(bool))]
        // Convert to Int -- All numerics will succeed
        [TestCase(false, typeof(int), default(int))]
        [TestCase("Test string", typeof(int), default(int))]
        // Convert to Float -- All numerics will succeed
        [TestCase(false, typeof(float), default(float))]
        [TestCase("Test string", typeof(float), default(float))]
        // Convert to Double -- All numerics will succeed
        [TestCase(false, typeof(double), default(double))]
        [TestCase("Test string", typeof(double), default(double))]
        // Convert to Object -- none will fail
        // Convert to String -- all will call .ToString() and succeed
        public void TryConvertTo_InvalidCast_ReturnsDefault(object source, Type targetType, object expected)
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
        public void TryConvertTo_ValidCast_ConvertsCorrectly(object source, Type targetType, object expected)
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
