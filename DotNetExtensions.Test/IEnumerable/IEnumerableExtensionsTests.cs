
using DotNetExtensions.IEnumerable;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetExtensions.Test.IEnumerable
{
  [TestFixture]
  public class IEnumerableExtensionsTests
  {
    #region ForEach

    [Test]
    public void ForEach_NullSource_ThrowsException()
    {
      // Arrange
      IEnumerable<object> uut = null;
      Action<object> action = (x) => { };

      // Act - Assert
      Assert.Throws<ArgumentNullException>(() => uut.ForEach(action));
    }

    [Test]
    public void ForEach_NullAction_ThrowsException()
    {
      // Arrange
      IEnumerable<object> uut = new List<object>();
      Action<object> action = null;

      // Act - Assert
      Assert.Throws<ArgumentNullException>(() => uut.ForEach(action));
    }

    [Test]
    public void ForEach_NonNullArgs_DoesNotThrow()
    {
      // Arrange
      IEnumerable<object> uut = new List<object>();
      Action<object> action = (x) => { };

      // Act - Assert
      Assert.DoesNotThrow(() => uut.ForEach(action));
    }

    #endregion

    #region RemoveFromEnd

    [Test]
    public void RemoveFromEnd_NullSource_ThrowsException()
    {
      // Arrange
      IEnumerable<object> uut = null;
      int count = 0;

      // Act - Assert
      Assert.Throws<ArgumentNullException>(() => uut.RemoveFromEnd(count));
    }

    [Test]
    public void RemoveFromEnd_NegativeCount_ThrowsException()
    {
      // Arrange
      IEnumerable<object> uut = new List<object>();
      int count = -1;

      // Act - Assert
      Assert.Throws<ArgumentOutOfRangeException>(() => uut.RemoveFromEnd(count));
    }

    [Test]
    public void RemoveFromEnd_ValidArgs_DoesNotThrow()
    {
      // Arrange
      IEnumerable<object> uut = new List<object>();
      int count = 0;

      // Act - Assert
      Assert.DoesNotThrow(() => uut.RemoveFromEnd(count));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(3)]
    [TestCase(4)]
    public void RemoveFromEnd_CountLessThanEqualToLength_RemovesFromEnd(int toRemove)
    {
      // Arrange
      IEnumerable<object> uut = new List<object>()
      {
        "1",
        "2",
        "3",
        "4"
      };

      var expected = uut.Reverse().Skip(toRemove).Reverse();
      var expectedLength = expected.Count();

      Assume.That(expectedLength, Is.Not.Negative);

      // Act
      uut = uut.RemoveFromEnd(toRemove);

      // Assert
      Assert.That(uut.Count(), Is.EqualTo(expectedLength));

      for (var i = 0; i < uut.Count(); i ++)
      {
        Assert.That(uut.ElementAt(i), Is.EqualTo(expected.ElementAt(i)));
      }
    }

    [TestCase(5)]
    [TestCase(15)]
    [TestCase(150)]
    public void RemoveFromEnd_CountGreaterThanLength_RemovesAll(int toRemove)
    {
      // Arrange
      IEnumerable<object> uut = new List<object>()
      {
        "1",
        "2",
        "3",
        "4"
      };

      Assume.That(uut.Count(), Is.LessThan(toRemove));

      // Act
      uut = uut.RemoveFromEnd(toRemove);

      // Assert
      Assert.That(uut, Is.Empty);
    }

    #endregion
  }
}
