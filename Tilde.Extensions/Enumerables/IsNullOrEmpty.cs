namespace Tilde.Extensions;

public static partial class EnumerableExtensions {
  public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source) => source is null || !source.Any();
}
// Example Usage
/*
var isEmpty = Enumerable.Empty<int>().IsNullOrEmpty(); // true
var isEmpty = new[] { 1, 2, 3 }.IsNullOrEmpty(); // false
var isEmpty = ((IEnumerable<int>)null).IsNullOrEmpty(); // true
*/
