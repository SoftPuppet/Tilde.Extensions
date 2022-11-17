using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tilde.Extensions.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tilde.Extensions.Utilities.Tests
{
    [TestClass()]
    public class FuzzyCompareTests
    {
        [TestMethod()]
        public void DistanceTest()
        {
            Assert.AreEqual(16, FuzzyCompare.Distance("jo davidson", "joanna montgomerie-davidson"));
            Assert.AreEqual(13, FuzzyCompare.Distance("luis alvarez", "luis miguel alvarez perez"));
            Assert.AreEqual(4, FuzzyCompare.Distance("mike taitoko", "michael taitoko"));
            Assert.AreEqual(3, FuzzyCompare.Distance("rukumoana m. schaafhausen", "rukumoana schaafhausen"));
            Assert.AreEqual(0, FuzzyCompare.Distance("wynnis armour", "wynnis armour"));
            Assert.AreEqual(18, FuzzyCompare.Distance("mohammed al nuaimi", ""));
            Assert.AreEqual(16, FuzzyCompare.Distance("benedikt mangold", ""));
            Assert.AreEqual(18, FuzzyCompare.Distance("", "mohammed al nuaimi"));
            Assert.AreEqual(16, FuzzyCompare.Distance("", "benedikt mangold"));
        }
    }
}