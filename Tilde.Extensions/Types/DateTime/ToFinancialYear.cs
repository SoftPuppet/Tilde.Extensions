using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Tilde.Extensions.Types
{
    public static partial class DateTimeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="delimiter"></param>
        /// <param name="firstMonthOfFinancialYear"></param>
        /// <returns></returns>
        /// 

        public static string ToFinancialYear([DisallowNull] this System.DateTime source, string delimiter = "-", int firstMonthOfFinancialYear = 4)
        {
            // TODO: use firstMonthOfFinancialYear to do the conversion based on different financial systems
            // Currently the first month of the financial year is April = 4
            var sb = new StringBuilder();
            var currentFinancialYear = source.Month >= 4 ? source.Year : (source.Year - 1);
            var nextFinancialYear = (currentFinancialYear + 1).ToString().Remove(0, 2);
            sb.Append(currentFinancialYear)
                .Append(delimiter)
                .Append(nextFinancialYear);
            return sb.ToString();
        }
    }
}