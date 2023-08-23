using System;
using System.Text;

namespace Tilde.Extensions
{
    public static partial class DateTimeExtensions
    {
        /// <summary>
        /// Calculates the financial year based on the given date, the start month of the financial year, and a format string.
        /// </summary>
        /// <param name="source">The date for which to calculate the financial year.</param>
        /// <param name="startMonth">The starting month of the financial year, represented as an enum value.</param>
        /// <param name="format">A format string that specifies how to format the output, using 'SS' for the start year (short), 'SSSS' for the start year (long), 'EE' for the end year (short), and 'EEEE' for the end year (long). Default format is "Financial Year SSSS-EE-".</param>
        /// <returns>A string representing the financial year in the specified format.</returns>
        public static string GetFinancialYear(this DateTime source, Month startMonth = Month.April, string format = "FYSSSS-EE")
        {
            int financialYearStart, financialYearEnd = source.Year;

            // If FY is the same as the calendar year, it is a special case
            if (startMonth == Month.January)
            {
                financialYearStart = financialYearEnd = source.Year;
            }
            else if (source.Month < (int)startMonth && source.Month > (int)Month.January)
            {
                // Check if the date falls before the start of the financial year
                // This check ensures that the source date falls before the Year End.
                financialYearEnd = source.Year;
                financialYearStart = financialYearEnd - 1;
            }
            else
            {
                financialYearEnd = source.Year + 1;
                financialYearStart = financialYearEnd - 1;
            }

            string financialYearEndShort = financialYearEnd.ToString().Substring(financialYearEnd.ToString().Length - 2);
            string financialYearStartShort = financialYearStart.ToString().Substring(financialYearStart.ToString().Length - 2);

            // Build the result string by parsing the format string
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < format.Length; i++)
            {
                if (i < format.Length - 1 && format[i] == 'S' && format[i + 1] == 'S')
                {
                    if (i < format.Length - 3 && format[i + 2] == 'S' && format[i + 3] == 'S')
                    {
                        // Replace "SSSS" with the full start year
                        result.Append(financialYearStart);
                        i += 3;
                    }
                    else
                    {
                        // Replace "SS" with the short start year
                        result.Append(financialYearStartShort);
                        i++;
                    }
                }
                else if (i < format.Length - 1 && format[i] == 'E' && format[i + 1] == 'E')
                {
                    if (i < format.Length - 3 && format[i + 2] == 'E' && format[i + 3] == 'E')
                    {
                        // Replace "EEEE" with the full end year
                        result.Append(financialYearEnd);
                        i += 3;
                    }
                    else
                    {
                        // Replace "EE" with the short end year
                        result.Append(financialYearEndShort);
                        i++;
                    }
                }
                else
                {
                    // Preserve all other characters
                    result.Append(format[i]);
                }
            }
            return result.ToString();
        }
    }
}