namespace Tilde.ExtensionsTests; 

[TestClass()]
public class CleanNameTests {
  [TestMethod()]
  [Obsolete]
  public void CleanTest() {
    Assert.AreEqual("william bradley pitt", StringExtensions.NormalizePersonName("William Bradley Pitt"));
    //Assert.AreEqual("peiyu yu", NomalizePersonName.Clean("Pei-yu Yu"));
    //Assert.AreEqual("william (bill) birnie", NomalizePersonName.Clean("William (Bill) Birnie"));
    //Assert.AreEqual("renee smith", NomalizePersonName.Clean("Renée Smith"));
    //Assert.AreEqual("hinerangi raumatituua", NomalizePersonName.Clean("Hinerangi Raumati-Tu’ua"));
    //Assert.AreEqual("mary oconnor", NomalizePersonName.Clean("Mary O'Connor"));
    //Assert.AreEqual("ewan tempero", NomalizePersonName.Clean("Dr. Ewan Tempero"));
    //Assert.AreEqual("robert downey", NomalizePersonName.Clean("Robert Downey Jr"));
    //Assert.AreEqual("maryjane ng", NomalizePersonName.Clean("Mary-Jane Ng"));
    //Assert.AreEqual("hon smith", NomalizePersonName.Clean("Dame Hon Smith"));
    //Assert.AreEqual("john smith", NomalizePersonName.Clean("John Smith"));
    //Assert.AreEqual("charles", NomalizePersonName.Clean("Charles III"));
    //Assert.AreEqual("elizabeth", NomalizePersonName.Clean("Elizabeth II"));
    //Assert.AreEqual("george", NomalizePersonName.Clean("George IV"));
  }
}