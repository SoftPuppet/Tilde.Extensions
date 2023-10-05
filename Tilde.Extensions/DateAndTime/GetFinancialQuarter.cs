namespace Tilde.Extensions; 

public static partial class DateTimeExtensions {
  /// <summary>
  /// Calculates the financial quarter of a given date based on the start month of the financial year.
  /// </summary>
  /// <param name="source">The date for which the financial quarter needs to be determined.</param>
  /// <param name="startMonthOfFY">The start month of the financial year, defaulting to April.</param>
  /// <returns>The financial quarter as an integer (1 to 4).</returns>
  public static int GetFinancialQuarter(this DateTime source, Month startMonthOfFY = Month.April) {
    int startMonth = (int)startMonthOfFY;
    // Shift months based on the start of the financial year
    int shiftedMonth = ((source.Month - startMonth + 12) % 12) + 1;
    // Determine the quarter
    return (shiftedMonth - 1) / 3 + 1;
  }

  /// <summary>
  /// Calculates the calendar quarter of a given date based on the standard calendar year.
  /// </summary>
  /// <param name="source">The date for which the calendar quarter needs to be determined.</param>
  /// <returns>The calendar quarter as an integer (1 to 4).</returns>
  public static int GetCalendarQuarter(this DateTime source) {
    return (source.Month - 1) / 3 + 1;
  }
}
