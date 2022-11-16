using System.Collections.Generic;

namespace Tilde.Extensions.Enumerable
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source) => source ?? System.Linq.Enumerable.Empty<T>();
    }
}
