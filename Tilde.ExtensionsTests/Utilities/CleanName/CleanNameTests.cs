using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tilde.Extensions.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilde.Extensions.Utilities.Tests
{
    [TestClass()]
    public class CleanNameTests
    {
        [TestMethod()]
        public void CleanTest()
        {
            Assert.AreEqual("william bradley pitt", CleanName.Clean("William Bradley Pitt"));
            Assert.AreEqual("peiyu yu", CleanName.Clean("Pei-yu Yu"));
            Assert.AreEqual("william (bill) birnie", CleanName.Clean("William (Bill) Birnie"));
            Assert.AreEqual("renee smith", CleanName.Clean("Renée Smith"));
            Assert.AreEqual("hinerangi raumatituua", CleanName.Clean("Hinerangi Raumati-Tu’ua"));
            Assert.AreEqual("mary oconnor", CleanName.Clean("Mary O'Connor"));
            Assert.AreEqual("ewan tempero", CleanName.Clean("Dr. Ewan Tempero"));
            Assert.AreEqual("robert downey", CleanName.Clean("Robert Downey Jr"));
            Assert.AreEqual("maryjane ng", CleanName.Clean("Mary-Jane Ng"));
            Assert.AreEqual("hon smith", CleanName.Clean("Dame Hon Smith"));
            Assert.AreEqual("john smith", CleanName.Clean("John Smith"));
            Assert.AreEqual("charles", CleanName.Clean("Charles III"));
            Assert.AreEqual("elizabeth", CleanName.Clean("Elizabeth II"));
            Assert.AreEqual("george", CleanName.Clean("George IV"));
        }
    }
}