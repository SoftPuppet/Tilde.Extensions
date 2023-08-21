﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Extensions.Types;

namespace Tilde.ExtensionsTests;

[TestClass]
public class ExtractFirstValidJsonTests
{
    [TestMethod]
    public void Test_JsonString()
    {
        string source = "{\"name\":\"John\",\"age\":30}";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual(source, result);
    }

    [TestMethod]
    public void Test_JsonArrayString()
    {
        string source = "[{\"name\":\"John\"},{\"name\":\"Jane\"}]";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual(source, result);
    }

    [TestMethod]
    public void Test_TextBeforeJsonString()
    {
        string source = "Some text before {\"name\":\"John\",\"age\":30}";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual("{\"name\":\"John\",\"age\":30}", result);
    }

    [TestMethod]
    public void Test_TextBeforeAndAfterJsonString()
    {
        string source = "Before {\"name\":\"John\",\"age\":30} After";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual("{\"name\":\"John\",\"age\":30}", result);
    }

    [TestMethod]
    public void Test_PartialJsonString()
    {
        string source = "Before {\"name\":\"John\",\"age\":30 After";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual(string.Empty, result);
    }

    [TestMethod]
    public void Test_ComplexJsonString()
    {
        string source = "Before {\"person\":{\"name\":\"John\",\"age\":30,\"address\":{\"city\":\"New York\"}}} After";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual("{\"person\":{\"name\":\"John\",\"age\":30,\"address\":{\"city\":\"New York\"}}}", result);
    }

    [TestMethod]
    public void Test_ComplexJsonArray()
    {
        string source = "Before [{\"name\":\"John\",\"age\":30},{\"name\":\"Jane\",\"age\":25}] After";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual("[{\"name\":\"John\",\"age\":30},{\"name\":\"Jane\",\"age\":25}]", result);
    }

    [TestMethod]
    public void Test_EmptyString()
    {
        string source = "";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual(string.Empty, result);
    }

    [TestMethod]
    public void Test_NoJsonString()
    {
        string source = "This text has no JSON";
        string result = source.ExtractFirstValidJson();
        Assert.AreEqual(string.Empty, result);
    }

    // Test the Deserialize Extension Method
    [TestMethod]
    public void Test_DeserializeSimpleJsonObject()
    {
        string source = "{\"name\":\"John\",\"age\":30}";
        Person result = source.ExtractFirstValidJson<Person>();
        Assert.AreEqual("John", result.name);
        Assert.AreEqual(30, result.age);
    }

    [TestMethod]
    public void Test_DeserializeJsonArrayToObjectList()
    {
        string source = "[{\"name\":\"John\"},{\"name\":\"Jane\"}]";
        List<Person> result = source.ExtractFirstValidJson<List<Person>>();
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("John", result[0].name);
        Assert.AreEqual("Jane", result[1].name);
    }

    [TestMethod]
    public void Test_DeserializePartialJsonString()
    {
        string source = "Before {\"name\":\"John\",\"age\":30 After";
        Person result = source.ExtractFirstValidJson<Person>();
        Assert.AreEqual(null, result);
    }

    [TestMethod]
    public void Test_DeserializeComplexJsonString()
    {
        string source = "Before {\"person\":{\"name\":\"John\",\"age\":30,\"address\":{\"city\":\"New York\"}}} After";
        ComplexPerson result = source.ExtractFirstValidJson<ComplexPerson>();
        Assert.AreEqual("John", result.person.name);
        Assert.AreEqual(30, result.person.age);
        Assert.AreEqual("New York", result.person.address.city);
    }

    [TestMethod]
    public void Test_DeserializeComplexJsonArray()
    {
        string source = "Before [{\"name\":\"John\",\"age\":30},{\"name\":\"Jane\",\"age\":25}] After";
        List<Person> result = source.ExtractFirstValidJson<List<Person>>();
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("John", result[0].name);
        Assert.AreEqual(30, result[0].age);
        Assert.AreEqual("Jane", result[1].name);
        Assert.AreEqual(25, result[1].age);
    }

    [TestMethod]
    public void Test_DeserializeEmptyString()
    {
        string source = "";
        Person result = source.ExtractFirstValidJson<Person>();
        Assert.AreEqual(null, result);
    }

    [TestMethod]
    public void Test_DeserializeNoJsonString()
    {
        string source = "This text has no JSON";
        Person result = source.ExtractFirstValidJson<Person>();
        Assert.AreEqual(null, result);
    }

    // Definitions for classes used in the tests
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Person
    {
        public string name { get; set; }
        public int age { get; set; }
    }

    public class ComplexPerson
    {
        public Person person { get; set; }

        public class Person
        {
            public string name { get; set; }
            public int age { get; set; }
            public Address address { get; set; }
        }

        public class Address
        {
            public string city { get; set; }
        }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

