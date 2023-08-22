using System.Collections.Generic;
using System.Text.Json;

namespace Tilde.Extensions.Strings
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Determines if the source string contains any valid JSON object, and returns all json strings.
        /// </summary>
        /// <param name="source">The string to be scanned for valid JSON objects.</param>
        /// <returns>List of json strings found in the source. Empty if no json string is found.</returns>
        public static IEnumerable<string> ExtractAllValidJson(this string source)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(source))
            {
                return result;
            }

            int bracketCount = 0;
            int startIndex = -1;
            bool inString = false;

            for (int i = 0; i < source.Length; i++)
            {
                char c = source[i];

                if (c == '\"' && (i == 0 || source[i - 1] != '\\'))
                {
                    inString = !inString;
                }

                if (!inString) // Only process brackets outside strings
                {
                    if (c == '{' || c == '[')
                    {
                        if (bracketCount == 0)
                        {
                            startIndex = i;
                        }
                        bracketCount++;
                    }
                    else if (c == '}' || c == ']')
                    {
                        bracketCount--;
                        if (bracketCount == 0 && startIndex != -1)
                        {
                            var jsonSlice = source[startIndex..(i + 1)];
                            try
                            {
                                JsonSerializer.Deserialize<object>(jsonSlice);
                                result.Add(jsonSlice);
                            }
                            catch (JsonException)
                            {
                                /* Not a valid JSON, continue search */
                            }
                            startIndex = -1; // Reset start index
                        }
                    }
                }
            }

            return result;
        }

    }
}
