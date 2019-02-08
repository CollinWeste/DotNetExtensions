
using DotNetExtensions.IEnumerable;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
  }
}
