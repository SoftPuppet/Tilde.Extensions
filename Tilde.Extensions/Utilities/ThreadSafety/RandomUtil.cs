namespace Tilde.Extensions.Utilities {
  internal static partial class RandomUtil {
    private static readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(() => new Random());

    internal static int Next(int minValue, int maxValue) {
      return _random.Value?.Next(minValue, maxValue) ?? new Random().Next(minValue, maxValue);
    }
  }
}
