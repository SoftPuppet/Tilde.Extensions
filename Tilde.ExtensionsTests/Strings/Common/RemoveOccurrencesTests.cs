using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Extensions.Strings;

namespace Tilde.ExtensionsTests.Strings.Common
{
    [TestClass]
    public class RemoveOccurencesTests
    {
        [TestMethod]
        public void Test_RemoveOccurrences_Anywhere()
        {
            string source = "I am a new world, World world";
            string result = source.RemoveOccurrences(RemovalMode.Anywhere, false, "world");
            Assert.AreEqual("I am a new , World ", result);
        }

        [TestMethod]
        public void Test_RemoveOccurrences_StartOnly()
        {
            string source = "world world world, I am a new world";
            string result = source.RemoveOccurrences(RemovalMode.StartOnly, false, "world");
            Assert.AreEqual(", I am a new world", result);
        }

        [TestMethod]
        public void Test_RemoveOccurrences_EndOnly()
        {
            string source = "I am a new world, world world";
            string result = source.RemoveOccurrences(RemovalMode.EndOnly, false, "world");
            Assert.AreEqual("I am a new world,", result);
        }

        [TestMethod]
        public void Test_RemoveOccurrences_MultipleStrings()
        {
            string source = "I am a new world, hello world, world world";
            string result = source.RemoveOccurrences(RemovalMode.Anywhere, false, "world", "hello");
            Assert.AreEqual("I am a new ,  ,  ", result);
        }

        [TestMethod]
        public void Test_RemoveOccurrences_CaseInsensitive()
        {
            string source = "I am a new WoRld, wOrld world";
            string result = source.RemoveOccurrences(RemovalMode.Anywhere, true, "world");
            Assert.AreEqual("I am a new ,  ", result);
        }

        [TestMethod]
        public void Test_RemoveOccurrences_NullSource()
        {
            string source = null!;
            string result = source.RemoveOccurrences(RemovalMode.Anywhere, false, "world");
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Test_RemoveOccurrences_EmptySource()
        {
            string source = "";
            string result = source.RemoveOccurrences(RemovalMode.Anywhere, false, "world");
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Test_RemoveOccurrences_EmptyStringToRemove()
        {
            string source = "I am a new world, world world";
            string result = source.RemoveOccurrences(RemovalMode.Anywhere, false, "");
            Assert.AreEqual(source, result);
        }

        [TestMethod]
        public void TestRemoveOccurrences_WithWhitespaceSource_WhitespaceToRemove()
        {
            string source = "   ";
            string stringToRemove = " ";
            string result = source.RemoveOccurrences(RemovalMode.Anywhere, true, stringToRemove);
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void TestRemoveOccurrences_WithWhitespaceSource_DoubleWhitespaceToRemove()
        {
            string source = "     ";
            string stringToRemove = "  ";
            string result = source.RemoveOccurrences(RemovalMode.Anywhere, true, stringToRemove);
            Assert.AreEqual(" ", result);
        }
    }
}
