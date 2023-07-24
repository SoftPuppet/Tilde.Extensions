using System;
using System.Collections.Generic;
using System.Text;

namespace Tilde.Extensions.Types
{
    public static partial class StringExtensions
    {
        public static string RemoveAllOccurrencesOfSingleString(this string @this, string stringToRemove)
        {
            if (string.IsNullOrEmpty(@this)) return string.Empty;
            if (string.IsNullOrEmpty(stringToRemove)) return @this;
            int indexOfStringToBeRemoved;
            do
            {
                indexOfStringToBeRemoved = @this.ToLower().IndexOf(stringToRemove);
                if (indexOfStringToBeRemoved >= 0) @this = @this.Remove(indexOfStringToBeRemoved, stringToRemove.Length);
            } while (indexOfStringToBeRemoved != -1); // index = -1 when the string is not found in the target
            return @this;
        }
    }
}
