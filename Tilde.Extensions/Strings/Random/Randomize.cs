using Tilde.Extensions.Utilities;

namespace Tilde.Extensions; 
public static partial class StringExtensions {
  /// <summary>
  /// Randomizes the characters of the input string and returns the resulting string.
  /// </summary>
  /// <param name="source">The string to randomize.</param>
  /// <param name="original">Outputs the original string before randomization.</param>
  /// <param name="stackAllocThreshold">The maximum size for stack allocation. If the source string length is less than this threshold, stack allocation is used; otherwise, heap allocation is used. Defaults to 512.</param>
  /// <returns>The randomized string with characters from the original source string.</returns>
  public static string Randomize(this string source, out string original, int stackAllocThreshold = 512) {
    original = source;
    if (string.IsNullOrEmpty(source)) {
      return source;
    }

    Span<char> span = source.Length <= stackAllocThreshold
        ? stackalloc char[source.Length]
        : new char[source.Length];

    source.AsSpan().CopyTo(span);

    for (int i = span.Length - 1; i > 0; i--) {
      int j = RandomUtil.Next(0, i + 1);
      char temp = span[i];
      span[i] = span[j];
      span[j] = temp;
    }

    return new string(span);
  }
}
