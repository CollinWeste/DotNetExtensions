using DotNetExtensions.IEnumerable;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions.Test.IEnumerable
{
    [TestFixture]
    internal class CompareTests
    {
        [Test]
        public void Compare_NullSource_ThrowsException()
        {
            // Arrange
            IEnumerable<int> source = null;
            IEnumerable<int> compareTo = new List<int>() { 1, 2, 3 };
            Func<int, int, int> comparer = (a, b) => a.CompareTo(b);

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => source.Compare(null));
            Assert.Throws<ArgumentNullException>(() => source.Compare(compareTo));

            Assert.Throws<ArgumentNullException>(() => source.Compare(null, comparer));
            Assert.Throws<ArgumentNullException>(() => source.Compare(compareTo, comparer));
        }

        [Test]
        public void Compare_NullCompareTo_ThrowsException()
        {
            // Arrange
            IEnumerable<int> source = new List<int>() { 1, 2, 3 };
            IEnumerable<int> compareTo = null;
            Func<int, int, int> comparer = (a, b) => a.CompareTo(b);

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => source.Compare(compareTo));
            Assert.Throws<ArgumentNullException>(() => source.Compare(compareTo, comparer));
        }

        [Test]
        public void Compare_NonNullArgs_DoesNotThrow()
        {
            // Arrange
            IEnumerable<int> source = new List<int>() { 1, 2, 3 };
            IEnumerable<int> compareTo = new List<int>() { 1, 2, 3 };
            Func<int, int, int> comparer = (a, b) => a.CompareTo(b);

            // Act - Assert
            Assert.DoesNotThrow(() => source.Compare(compareTo));
            Assert.DoesNotThrow(() => source.Compare(compareTo, comparer));
        }

        [Test]
        public void Compare_WithComparable_CallsCompareTo()
        {
            // Arrange
            var comparableMock1 = new Mock<IComparable>();
            comparableMock1.Setup(x => x.CompareTo(It.IsAny<IComparable>()))
                .Returns(0)
                .Verifiable();

            var comparableMock2 = new Mock<IComparable>();
            comparableMock2.Setup(x => x.CompareTo(It.IsAny<IComparable>()))
                .Returns(0)
                .Verifiable();

            IEnumerable<IComparable> source = new List<IComparable> { comparableMock1.Object };
            IEnumerable<IComparable> compareTo = new List<IComparable> { comparableMock2.Object };
            Func<IComparable, IComparable, int> comparer = (a, b) => a.CompareTo(b);

            try
            {
                comparableMock1.Verify(x => x.CompareTo(It.IsAny<IComparable>()), Times.Never());
                comparableMock2.Verify(x => x.CompareTo(It.IsAny<IComparable>()), Times.Never());
            }
            catch (MockException e)
            {
                Assume.That(false, e.Message);
            }

            // Act
            source.Compare(compareTo, comparer);

            // Assert
            comparableMock1.Verify(x => x.CompareTo(It.IsAny<IComparable>()), Times.AtLeastOnce());
            comparableMock2.Verify(x => x.CompareTo(It.IsAny<IComparable>()), Times.AtLeastOnce());
        }

        [Test]
        public void Compare_WithCompareDelegate_CallsDelegateOnEach()
        {
            // Arrange
            var delegateCalledCount = 0;

            Func<int, int, int> comparer =
                (a, b) =>
                {
                    delegateCalledCount++;
                    return 0;
                };

            IEnumerable<int> source = new List<int>() { 1, 2, 3, 4, 5 };
            IEnumerable<int> compareTo = new List<int>() { 1, 2, 3, 4, 5 };

            Assume.That(delegateCalledCount, Is.Zero);

            // Act
            source.Compare(compareTo, comparer);

            // Assert
            Assert.That(delegateCalledCount, Is.EqualTo(source.Count()));
        }

        [Test]
        public void Compare_ClassSameList_AreEqual()
        {
            // Arrange
            IEnumerable<string> source = new List<string>()
            {
                "1", "2", "3", "4", "5"
            };
            IEnumerable<string> compareTo = new List<string>()
            {
                "1", "2", "3", "4", "5"
            };

            Assume.That(source, Is.EqualTo(compareTo));
            Assume.That(source, Is.Not.SameAs(compareTo));

            // Act
            var result = source.Compare(compareTo);

            // Assert
            Assert.That(result, Is.Zero);
        }

        [TestCase("1,2,3,4")]
        [TestCase("0,2,3,4,5")]
        [TestCase("1,2,2,4,5")]
        [TestCase("1,2,3,4,4")]
        public void Compare_ClassLowerList_IsLessThanZero(string testCase)
        {
            // Arrange
            IEnumerable<string> source = testCase.Split(',');
            IEnumerable<string> compareTo = new List<string>()
            {
                "1", "2", "3", "4", "5"
            };

            Assume.That(source, Is.Not.EqualTo(compareTo));

            // Act
            var result = source.Compare(compareTo);

            // Assert
            Assert.That(result, Is.LessThan(0));
        }

        [TestCase("2,2,3,4,5")]
        [TestCase("1,2,4,4,5")]
        [TestCase("1,2,3,4,6")]
        [TestCase("1,2,3,4,5,6")]
        public void Compare_ClassGreaterList_IsHigherThanZero(string testCase)
        {
            // Arrange
            IEnumerable<string> source = testCase.Split(',');
            IEnumerable<string> compareTo = new List<string>()
            {
                "1", "2", "3", "4", "5"
            };

            Assume.That(source, Is.Not.EqualTo(compareTo));

            // Act
            var result = source.Compare(compareTo);

            // Assert
            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Compare_StructSameList_AreEqual()
        {
            // Arrange
            IEnumerable<int> source = new List<int>() { 1, 2, 3, 4, 5 };
            IEnumerable<int> compareTo = new List<int>() { 1, 2, 3, 4, 5 };

            Assume.That(source, Is.EqualTo(compareTo));
            Assume.That(source, Is.Not.SameAs(compareTo));

            // Act
            var result = source.Compare(compareTo);

            // Assert
            Assert.That(result, Is.Zero);
        }

        [TestCase("1,2,3,4")]
        [TestCase("0,2,3,4,5")]
        [TestCase("1,2,2,4,5")]
        [TestCase("1,2,3,4,4")]
        public void Compare_StructLowerList_IsLessThanZero(string testCase)
        {
            // Arrange
            IEnumerable<int> source = testCase.Split(',').Select(x => int.Parse(x));
            IEnumerable<int> compareTo = new List<int>() { 1, 2, 3, 4, 5 };

            Assume.That(source, Is.Not.EqualTo(compareTo));

            // Act
            var result = source.Compare(compareTo);

            // Assert
            Assert.That(result, Is.LessThan(0));
        }

        [TestCase("2,2,3,4,5")]
        [TestCase("1,2,4,4,5")]
        [TestCase("1,2,3,4,6")]
        [TestCase("1,2,3,4,5,6")]
        public void Compare_StructGreaterList_IsHigherThanZero(string testCase)
        {
            // Arrange
            IEnumerable<int> source = testCase.Split(',').Select(x => int.Parse(x));
            IEnumerable<int> compareTo = new List<int>() { 1, 2, 3, 4, 5 };

            Assume.That(source, Is.Not.EqualTo(compareTo));

            // Act
            var result = source.Compare(compareTo);

            // Assert
            Assert.That(result, Is.GreaterThan(0));
        }
    }
}
