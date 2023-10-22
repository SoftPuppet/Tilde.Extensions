namespace Tilde.Extensions;

public static partial class EnumerableExtensions {
  public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source) => source is null || !source.Any();
}
