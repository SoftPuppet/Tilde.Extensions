using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Extensions.Strings;

namespace Tilde.ExtensionsTests.Strings
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void TestRandomize()
        {
            string original;
            string source = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomized = source.Randomize(out original, stackAllocThreshold: 512);

            // Check that the original string is output correctly
            Assert.AreEqual(source, original);

            // Check that the length is the same
            Assert.AreEqual(source.Length, randomized.Length);

            // Check that the characters are the same, just in a different order
            CollectionAssert.AreEquivalent(source.ToCharArray().ToList(), randomized.ToCharArray().ToList());
        }
    }
}
