using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Tilde.Extensions.Types
{
   public static partial class StringExtensions
   {
      public static bool ContainsValidJson([DisallowNull] this string source)
      {
         return Regex.Matches(source, @"\{(.|\s)*\}").Count > 0;
         //var json = JsonSerializer.Deserialize<object>(source);
      }
   }
}
