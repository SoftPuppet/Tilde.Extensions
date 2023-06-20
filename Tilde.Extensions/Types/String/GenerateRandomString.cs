using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Tilde.Extensions.Types.String
{
    public static partial class StringExtensions
    {
        private static Random random = new Random();
        public static string GenerateRandomString([DisallowNull] this string source, string prefixString, string suffixString, int length)
        {
            // Create a string of all possible characters we want to include in our random string.
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Generate a random string
            var randomString = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                randomString.Append(chars[random.Next(chars.Length)]);
            }

            // Return the prefixed and suffixed string
            return $"{prefixString}{randomString}{suffixString}";
        }
    }
}
