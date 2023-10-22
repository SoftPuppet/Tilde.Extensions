using System.Diagnostics.CodeAnalysis;

namespace Tilde.Extensions;

public static partial class EnumerableExtensions {
  public static bool TryPartition<T>(this IEnumerable<T> source,
                                     Func<T, bool> predicate,
                                     [MaybeNullWhen(false)]
                                     out (IEnumerable<T> Matched, IEnumerable<T> Unmatched) result) {
    if (source is null || predicate is null) {
      result = default;
      return false;
    }
    var matchedItems   = new List<T>();
    var unmatchedItems = new List<T>();
    foreach (var item in source) {
      if (predicate(item)) {
        matchedItems.Add(item);
      } else {
        unmatchedItems.Add(item);
      }
    }
    result = (matchedItems, unmatchedItems);
    return true;
  }
}

// Example
/*
  var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

  if (EnumerableExtensions.TryPartition(numbers, x => x % 2 == 0, out var result)) {
    Console.WriteLine("Partition successful!");

    Console.WriteLine("Matched items:");
    foreach (var item in result.Matched) {
      Console.WriteLine(item);
    }

    Console.WriteLine("Unmatched items:");
    foreach (var item in result.Unmatched) {
      Console.WriteLine(item);
    }
  } else {
    Console.WriteLine("Partition failed. Check your input parameters.");
  }
*/
