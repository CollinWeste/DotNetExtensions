using DotNetExtensions.IEnumerable;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions.Test.IEnumerable
{
    [TestFixture]
    public class IEnumerableExtensionsTests
    {
        #region Compare

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

        #endregion

        #region ForEach

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

        #endregion

        #region RemoveFromEnd

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

        #endregion
    }
}
