using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Tilde.Extensions
{
    public static partial class StringExtensions
    {
        private static readonly Regex titlesAndSuffixesRegex = new Regex(@"\b(dr|jr|sr|iii|ii|iv|dame)\b", RegexOptions.IgnoreCase);

        [Obsolete("Method is incomplete and will be replaced with a more generic version.")]
        public static string NormalizePersonName(this string source)
        {
            source = source.ToLowerInvariant().Normalize(NormalizationForm.FormD);
            source = RemoveDiacritics(source);
            source = RemoveSpecialCharacters(source);
            source = RemoveTitlesAndSuffixes(source);
            return source.Trim();
        }
        private static string RemoveDiacritics(string text)
        {
            var stringBuilder = new StringBuilder();
            foreach (char c in text)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string RemoveSpecialCharacters(string text)
        {
            return Regex.Replace(text, @"[-.'’]", "");
        }

        private static string RemoveTitlesAndSuffixes(string text)
        {
            return titlesAndSuffixesRegex.Replace(text, "");
        }
    }
}
