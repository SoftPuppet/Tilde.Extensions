namespace Tilde.ExtensionsTests.DateAndTime
{
    [TestClass]
    public class GetFinancialYearTests
    {
        [TestMethod]
        public void TestWithAprilStart()
        {
            Assert.AreEqual("FY2022-23", new DateTime(2023, 3, 31).GetFinancialYear());
            Assert.AreEqual("FY2023-24", new DateTime(2023, 4, 1).GetFinancialYear());
        }

        [TestMethod]
        public void TestWithJanuaryStart()
        {
            Assert.AreEqual("FY2023-23", new DateTime(2023, 12, 31).GetFinancialYear(Month.January));
            Assert.AreEqual("FY2024-24", new DateTime(2024, 1, 1).GetFinancialYear(Month.January));
        }

        [TestMethod]
        public void TestWithCustomFormat()
        {
            Assert.AreEqual("Financial Year 2022-23", new DateTime(2023, 3, 31).GetFinancialYear(format: "Financial Year SSSS-EE"));
        }

        [TestMethod]
        public void TestMultipleYears()
        {
            for (int year = 1910; year <= 2025; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    // Test logic here, depending on your specific requirements.
                    // You may need to define the expected result based on the input year and month.
                    // This can be done by constructing the expected result string based on your business logic
                    // or by calling a helper method that encapsulates that logic.
                    var date = new DateTime(year, month, 1);
                    var result = date.GetFinancialYear(Month.April, "FYSSSS-EEEE");
                    var expected = CalculateExpectedResult(year, month);
                    Assert.AreEqual(expected, result);
                }
            }
        }

        private string CalculateExpectedResult(int year, int month)
        {
            int financialYearEnd = year;

            // Check if the date falls before the start of the financial year (April)
            if (month < 4 && month > 1)
            {
                financialYearEnd = year;
            }
            else
            {
                financialYearEnd = year + 1;
            }

            return $"FY{financialYearEnd - 1}-{financialYearEnd}";
        }
    }
}
