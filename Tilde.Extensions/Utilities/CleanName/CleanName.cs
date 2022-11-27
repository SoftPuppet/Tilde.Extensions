using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace Tilde.Extensions.Utilities
{
    public static class CleanName
    {
        public static string Clean(string name)
        {
            name = name.ToLower();
            name = RemoveDiacritics(name);
            name = RemoveDash(name);
            name = RemoveDot(name);
            name = RemoveSingleQuote(name);
            name = RemoveTitleAndSuffix(name);
            return name.Trim();
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

        private static string RemoveDash(string text)
        {
            return text.Replace("-", "");
        }

        private static string RemoveDot(string text)
        {
            return text.Replace(".", "");
        }

        private static string RemoveSingleQuote(string text)
        {
            return text.Replace("\'", "").Replace("’", "");
        }

        private static string RemoveTitleAndSuffix(string text)
        {
            List<string> suffixList = new List<string> { "dr", "jr", "sr", "iii", "ii", "iv", "dame" };
            foreach (string suffix in suffixList)
            {
                if (text.Contains(string.Format("{0} ", suffix)) || text.Contains(string.Format(" {0}", suffix)))
                {
                    text = text.Replace(suffix, "").Trim();
                }
            }
            return text;
        }
    }
}
