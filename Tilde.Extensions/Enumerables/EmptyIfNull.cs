namespace Tilde.Extensions; 

public static partial class EnumerableExtensions {
  /// <summary>
  /// Returns an empty sequence if the input is null, otherwise returns the input itself.
  /// </summary>
  /// <param name="source">The source sequence.</param>
  /// <returns>An empty sequence if the input is null; otherwise, the input itself.</returns>
  public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source) => source ?? System.Linq.Enumerable.Empty<T>();
}
