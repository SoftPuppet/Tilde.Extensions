using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Extensions.Strings;

namespace Tilde.ExtensionsTests.Strings;

[TestClass]
public class HasValidJsonTests
{
    [TestMethod]
    public void TestSourceIsJsonString()
    {
        string source = "{\"key\":\"value\"}";
        Assert.IsTrue(source.HasValidJson());
    }

    [TestMethod]
    public void TestSourceIsJsonArrayString()
    {
        string source = "[{\"key\":\"value\"},{\"key2\":\"value2\"}]";
        Assert.IsTrue(source.HasValidJson());
    }

    [TestMethod]
    public void TestSourceHasTextBeforeJsonString()
    {
        string source = "Text before {\"key\":\"value\"}";
        Assert.IsTrue(source.HasValidJson());
    }

    [TestMethod]
    public void TestSourceHasTextBeforeAndAfterJsonString()
    {
        string source = "Text before {\"key\":\"value\"} text after";
        Assert.IsTrue(source.HasValidJson());
    }

    [TestMethod]
    public void TestSourceContainsPartialJsonString()
    {
        string source = "Text before {\"key\":\"value text after";
        Assert.IsFalse(source.HasValidJson());
    }

    [TestMethod]
    public void TestSourceContainsContentBeforeAndAfterComplexJsonString()
    {
        string source = "Text before {\"key\":{\"subkey\":\"value\"},\"array\":[1,2,3]} text after";
        Assert.IsTrue(source.HasValidJson());
    }

    [TestMethod]
    public void TestSourceContainsContentBeforeAndAfterComplexJsonArray()
    {
        string source = "Text before [{\"key\":{\"subkey\":\"value\"}},{\"key2\":\"value2\"}] text after";
        Assert.IsTrue(source.HasValidJson());
    }

    // More scenarios
    [TestMethod]
    public void TestEmptySource()
    {
        string source = "";
        Assert.IsFalse(source.HasValidJson());
    }

    [TestMethod]
    public void TestJsonStringWithBracketsInString()
    {
        string source = "{\"key\":\"value with { and } inside\"}";
        Assert.IsTrue(source.HasValidJson());
    }

    [TestMethod]
    public void TestJsonStringWithEscapedQuotes()
    {
        string source = "{\"key\":\"value with \\\" inside\"}";
        Assert.IsTrue(source.HasValidJson());
    }

    [TestMethod]
    public void TestNestedJsonObjects()
    {
        string source = "{\"key1\":{\"key2\":{\"key3\":\"value\"}}}";
        Assert.IsTrue(source.HasValidJson());
    }
}

