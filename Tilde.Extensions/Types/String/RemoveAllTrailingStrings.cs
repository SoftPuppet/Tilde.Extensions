using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tilde.Extensions.Types
{
    public static partial class StringExtensions
    {
        public static string RemoveAllTrailingStrings(this string @this, string[] stringsToRemove)
        {
            if (string.IsNullOrEmpty(@this)) return string.Empty;
            if (stringsToRemove == null) return @this;
            if (stringsToRemove.Count() == 0) return @this;

            foreach (string stringToken in stringsToRemove)
            {
                @this = RemoveAllOccurrencesOfSingleString(@this, stringToken);
            }
            return @this;
        }
    }
}
