using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tilde.Extensions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilde.Extensions.Types.Tests
{
    [TestClass()]
    public class DateTimeExtensionsTests
    {
        [TestMethod()]
        public void ToFinancialQuarterTest()
        {
            System.DateTime financialYearStart1 = new System.DateTime(2022, 6, 1);

            System.DateTime source1 = new System.DateTime(2000, 2, 29);
            System.DateTime source2 = new System.DateTime(2022, 9, 30);
            System.DateTime source3 = new System.DateTime(2023, 1, 1);
            System.DateTime source4 = new System.DateTime(2023, 6, 29);
            System.DateTime source5 = new System.DateTime(2023, 3, 29);
            System.DateTime source6 = new System.DateTime(2023, 3, 30);

            int quarter1 = source1.ToFinancialQuarter(financialYearStart1);
            int quarter2 = source2.ToFinancialQuarter(financialYearStart1);
            int quarter3 = source3.ToFinancialQuarter(financialYearStart1);
            int quarter4 = source4.ToFinancialQuarter(financialYearStart1);
            int quarter5 = source5.ToFinancialQuarter(financialYearStart1);
            int quarter6 = source6.ToFinancialQuarter(financialYearStart1);

            Assert.AreEqual(3, quarter1);
            Assert.AreEqual(2, quarter2);
            Assert.AreEqual(3, quarter3);
            Assert.AreEqual(1, quarter4);
            Assert.AreEqual(4, quarter5);
            Assert.AreEqual(4, quarter6);

            // ---------------------------------------------------------------------------

            System.DateTime financialYearStart2 = new System.DateTime(2023, 4, 1);

            System.DateTime source7 = new System.DateTime(2000, 2, 29);
            System.DateTime source8 = new System.DateTime(2024, 12, 30);
            System.DateTime source9 = new System.DateTime(2021, 7, 31);
            System.DateTime source10 = new System.DateTime(2008, 10, 30);
            System.DateTime source11 = new System.DateTime(2022, 4, 1);
            System.DateTime source12 = new System.DateTime(2030, 1, 1);

            int quarter7 = source7.ToFinancialQuarter(financialYearStart2);
            int quarter8 = source8.ToFinancialQuarter(financialYearStart2);
            int quarter9 = source9.ToFinancialQuarter(financialYearStart2);
            int quarter10 = source10.ToFinancialQuarter(financialYearStart2);
            int quarter11 = source11.ToFinancialQuarter(financialYearStart2);
            int quarter12 = source12.ToFinancialQuarter(financialYearStart2);

            Assert.AreEqual(4, quarter7);
            Assert.AreEqual(3, quarter8);
            Assert.AreEqual(2, quarter9);
            Assert.AreEqual(3, quarter10);
            Assert.AreEqual(1, quarter11);
            Assert.AreEqual(4, quarter12);
        }
    }
}