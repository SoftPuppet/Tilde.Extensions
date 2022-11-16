using System;
using System.Diagnostics.CodeAnalysis;

namespace Tilde.Extensions.Types
{
   public static partial class DateTimeExtensions
   {
      public static int ToFinancialQuarter([DisallowNull] this System.DateTime source, int finalMonthOfFinancialYear = 3)
      {
         throw new NotImplementedException();
      }

      public static int ToCalendarQuarter([DisallowNull] this System.DateTime source)
      {
         return (source.Month - 1) / 3 + 1;
      }
   }
}
