using System;
using System.Threading;

namespace Tilde.Extensions.Utilities
{
    internal static partial class RandomHelper
    {
        private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

        internal static int Next(int minValue, int maxValue)
        {
            return random.Value.Next(minValue, maxValue);
        }
    }
}
