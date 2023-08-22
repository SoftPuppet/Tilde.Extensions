using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Extensions.Enumerables;

namespace Tilde.ExtensionsTests.Enumerables;

[TestClass]
public class EmptyIfNullTests
{
    [TestMethod]
    public void EmptyIfNull_SourceIsNull_ReturnsEmptyEnumerable()
    {
        IEnumerable<int>? source = null;

        var result = source.EmptyIfNull();

        Assert.IsNotNull(result);
        Assert.IsFalse(result.Any());
    }

    [TestMethod]
    public void EmptyIfNull_SourceIsEmpty_ReturnsSameInstance()
    {
        var source = Enumerable.Empty<int>();

        var result = source.EmptyIfNull();

        Assert.AreSame(source, result);
    }

    [TestMethod]
    public void EmptyIfNull_SourceHasItems_ReturnsSameInstance()
    {
        var source = new List<int> { 1, 2, 3 };

        var result = source.EmptyIfNull();

        Assert.AreSame(source, result);
    }

    [TestMethod]
    public void EmptyIfNull_SourceHasItems_ItemsAreEqual()
    {
        var source = new List<int> { 1, 2, 3 };

        var result = source.EmptyIfNull();

        CollectionAssert.AreEqual(source, result.ToList());
    }
}
