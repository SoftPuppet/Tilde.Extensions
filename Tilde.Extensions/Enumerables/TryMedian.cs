using System.Diagnostics.CodeAnalysis;

namespace Tilde.Extensions;

public static partial class EnumerableExtensions {
  public static bool TryMedian<T>(this IEnumerable<T> source, [MaybeNullWhen(false)] out double median) where T : IConvertible {
    median = default;

    if (source.IsNullOrEmpty()) {
      return false;
    }

    var tempList = new List<double>();
    foreach (var item in source) {
      if (item == null || !double.TryParse(item.ToString(CultureInfo.InvariantCulture), out var temp)) {
        return false;
      }
      tempList.Add(temp);
    }

    if (tempList.Count == 0) {
      return false;
    }

    tempList.Sort();
    var count = tempList.Count;

    median = count % 2 == 0 ? (tempList[count / 2 - 1] + tempList[count / 2]) / 2 : tempList[count / 2];
    return true;
  }
}
