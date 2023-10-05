namespace Tilde.Extensions; 

public static partial class StringExtensions {
  /// <summary>
  /// Removes all occurrences of the specified strings from the source string based on the provided removal mode.
  /// </summary>
  /// <param name="source">The source string from which occurrences will be removed.</param>
  /// <param name="mode">Optional. Specifies the removal mode which controls how the strings are removed (whole string, start of string, or end of string). Default is WholeString.</param>
  /// <param name="ignoreCase">Optional. Determines whether the comparison is case-sensitive or case-insensitive. Default is false (case-sensitive).</param>
  /// <param name="stringsToRemove">The string values to be removed from the source string. Can be a single string or an array of strings.</param>
  /// <returns>The source string with all occurrences of the specified strings removed based on the removal mode. If the source is null or white space, returns an empty string.</returns>
  public static string RemoveOccurrences(this string source, RemovalMode mode = RemovalMode.Anywhere, bool ignoreCase = false, params string[] stringsToRemove) {
    if (string.IsNullOrEmpty(source) || stringsToRemove == null || stringsToRemove.Length == 0) {
      return source ?? string.Empty;
    }

    var comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
    var result = new StringBuilder(source);

    foreach (var stringToRemove in stringsToRemove) {
      if (string.IsNullOrEmpty(stringToRemove)) {
        continue;
      }

      int index;
      string resultString = result.ToString();

      switch (mode) {
        case RemovalMode.Anywhere:
          while ((index = resultString.IndexOf(stringToRemove, comparisonType)) != -1) {
            result.Remove(index, stringToRemove.Length);
            resultString = result.ToString();
          }
          break;
        case RemovalMode.StartOnly:
          while ((index = resultString.IndexOf(stringToRemove, comparisonType)) == 0) {
            result.Remove(index, stringToRemove.Length);
            int leadingWhitespaceCount = 0;
            while (leadingWhitespaceCount < result.Length && char.IsWhiteSpace(result[leadingWhitespaceCount])) {
              leadingWhitespaceCount++;
            }
            result.Remove(0, leadingWhitespaceCount);
            resultString = result.ToString();
          }
          break;
        case RemovalMode.EndOnly:
          while ((index = resultString.LastIndexOf(stringToRemove, comparisonType)) != -1 &&
                  index == result.Length - stringToRemove.Length) {
            result.Remove(index, stringToRemove.Length);
            int trailingWhitespaceCount = 0;
            while (trailingWhitespaceCount < result.Length && char.IsWhiteSpace(result[result.Length - 1 - trailingWhitespaceCount])) {
              trailingWhitespaceCount++;
            }
            result.Remove(result.Length - trailingWhitespaceCount, trailingWhitespaceCount);
            resultString = result.ToString();
          }
          break;
      }
    }

    return result.ToString();
  }
}
