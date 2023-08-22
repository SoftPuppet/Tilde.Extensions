using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Extensions.Strings;

namespace Tilde.ExtensionsTests.Strings;

[TestClass]
public class ExtractAllValidJsonTests
{
    [TestMethod]
    public void Test_ExtractAllValidJson_JsonString()
    {
        string source = "{\"name\":\"John\",\"age\":30}";
        List<string> result = source.ExtractAllValidJson().ToList().ToList();
        CollectionAssert.AreEqual(new List<string> { source }, result);
    }

    [TestMethod]
    public void Test_ExtractAllValidJson_JsonArrayString()
    {
        string source = "[{\"name\":\"John\"},{\"name\":\"Jane\"}]";
        List<string> result = source.ExtractAllValidJson().ToList().ToList();
        CollectionAssert.AreEqual(new List<string> { source }, result);
    }

    [TestMethod]
    public void Test_ExtractAllValidJson_TextBeforeJsonString()
    {
        string source = "Text before {\"name\":\"John\"}";
        List<string> result = source.ExtractAllValidJson().ToList();
        CollectionAssert.AreEqual(new List<string> { "{\"name\":\"John\"}" }, result);
    }

    [TestMethod]
    public void Test_ExtractAllValidJson_TextBeforeAndAfterJsonString()
    {
        string source = "Before {\"name\":\"John\"} After";
        List<string> result = source.ExtractAllValidJson().ToList();
        CollectionAssert.AreEqual(new List<string> { "{\"name\":\"John\"}" }, result);
    }

    [TestMethod]
    public void Test_ExtractAllValidJson_PartialJsonString()
    {
        string source = "Before {\"name\":\"John\" After";
        List<string> result = source.ExtractAllValidJson().ToList();
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void Test_ExtractAllValidJson_ComplexJsonString()
    {
        string source = "Before {\"person\":{\"name\":\"John\",\"age\":30,\"address\":{\"city\":\"New York\"}}} After";
        List<string> result = source.ExtractAllValidJson().ToList();
        CollectionAssert.AreEqual(new List<string> { "{\"person\":{\"name\":\"John\",\"age\":30,\"address\":{\"city\":\"New York\"}}}" }, result);
    }

    [TestMethod]
    public void Test_ExtractAllValidJson_ComplexJsonArray()
    {
        string source = "Before [{\"name\":\"John\"},{\"name\":\"Jane\"}] After";
        List<string> result = source.ExtractAllValidJson().ToList();
        CollectionAssert.AreEqual(new List<string> { "[{\"name\":\"John\"},{\"name\":\"Jane\"}]" }, result);
    }

    [TestMethod]
    public void Test_ExtractAllValidJson_MultipleJsonObjectsWithComplexEmbeddedObjects()
    {
        string source = "{\"person\":{\"name\":\"John\",\"age\":30}}{\"company\":{\"name\":\"ABC Corp\",\"location\":{\"city\":\"New York\"}}}";
        List<string> result = source.ExtractAllValidJson().ToList();
        CollectionAssert.AreEqual(new List<string>
    {
        "{\"person\":{\"name\":\"John\",\"age\":30}}",
        "{\"company\":{\"name\":\"ABC Corp\",\"location\":{\"city\":\"New York\"}}}"
    }, result);
    }

    [TestMethod]
    public void Test_ExtractAllValidJson_MultipleJsonArrayObjectsWithComplexEmbeddedObjectsAndArrays()
    {
        string source = "[{\"person\":{\"name\":\"John\",\"friends\":[\"Alice\",\"Bob\"]}},{\"company\":{\"employees\":[{\"name\":\"Jane\"},{\"name\":\"Jack\"}]}}]";
        List<string> result = source.ExtractAllValidJson().ToList();
        CollectionAssert.AreEqual(new List<string>
        {
            "[{\"person\":{\"name\":\"John\",\"friends\":[\"Alice\",\"Bob\"]}},{\"company\":{\"employees\":[{\"name\":\"Jane\"},{\"name\":\"Jack\"}]}}]"
        }, result);
    }
}
