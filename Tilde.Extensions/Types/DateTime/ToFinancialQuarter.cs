using System.Diagnostics.CodeAnalysis;

namespace Tilde.Extensions.Types
{
   public static partial class DateTimeExtensions
   {
      /*public static int ToFinancialQuarter([DisallowNull] this System.DateTime source, int finalMonthOfFinancialYear = 3)
      {
         throw new NotImplementedException();
      }*/

      public static int ToFinancialQuarter([DisallowNull] this System.DateTime sourceDate, System.DateTime financialYearStartDate)
      {
         // Determine the length of each quarter based on the financial year start date
         System.DateTime[] quarterEndDates = new System.DateTime[4];
         for (int i = 0; i < quarterEndDates.Length; i++)
         {
            if (i == 0)
            {
               quarterEndDates[i] = financialYearStartDate.AddMonths(3).AddDays(-1);
            }
            else if (i == 3)
            {
               quarterEndDates[i] = financialYearStartDate.AddYears(1).AddDays(-1);
            }
            else
            {
               quarterEndDates[i] = quarterEndDates[i - 1].AddMonths(3);
            }
         }

         // Determine which quarter each input date falls within based on its distance from the start of the financial year
         int quarter = 0;
         //DateTime compareDate = new DateTime(1,inputDate.Month,inputDate.Day);
         for (int i = 0; i < quarterEndDates.Length; i++)
         {
            if ((sourceDate.Month <= quarterEndDates[i].Month) && (sourceDate.Month >= quarterEndDates[i].Month - 3))
            {
               quarter = i + 1;
               break;
            }
         }
         return quarter;
      }

      public static int ToCalendarQuarter([DisallowNull] this System.DateTime source)
      {
         return (source.Month - 1) / 3 + 1;
      }
   }
}
