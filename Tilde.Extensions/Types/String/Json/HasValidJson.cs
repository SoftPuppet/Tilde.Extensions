using System.Text.Json;

namespace Tilde.Extensions.Types
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Determines if the source string contains any valid JSON object. The potential json string can be stored anywhere in the source string, not necessarily at the beginning or the end.
        /// </summary>
        /// <param name="source">The string to be scanned for valid JSON objects.</param>
        /// <returns>True if the source contains any valid JSON objects; otherwise, false.</returns>
        public static bool HasValidJson(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            int bracketCount = 0;
            int startIndex = -1;
            bool inString = false;

            for (int i = 0; i < source.Length; i++)
            {
                char c = source[i];

                // Handle quotes to distinguish brackets within strings
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
                                return true;
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
            return false;
        }

        /// <summary>
        /// Determines if the source string contains any valid JSON object, and returns the first found.
        /// </summary>
        /// <param name="source">The string to be scanned for valid JSON objects.</param>
        /// <param name="json">The first valid JSON string found, or null if no valid JSON is found.</param>
        /// <returns>True if the source contains any valid JSON objects; otherwise, false.</returns>
        //public static bool HasValidJson(this string source, out string? json)
        //{
        //    json = null; // Initialize the out parameter

        //    if (string.IsNullOrEmpty(source) || string.IsNullOrWhiteSpace(source))
        //    {
        //        return false;
        //    }

        //    int openingIndex = 0;

        //    while (openingIndex < source.Length)
        //    {
        //        int braceCount = 0;
        //        char currentChar = source[openingIndex];
        //        char matchingBrace = currentChar == '{' ? '}' : (currentChar == '[' ? ']' : '\0');

        //        if (matchingBrace != '\0')
        //        {
        //            int closingIndex = openingIndex;
        //            for (int i = openingIndex + 1; i < source.Length; i++)
        //            {
        //                if (source[i] == currentChar) braceCount++;
        //                else if (source[i] == matchingBrace) braceCount--;

        //                if (braceCount == -1)
        //                {
        //                    closingIndex = i;
        //                    break;
        //                }
        //            }
        //            if (closingIndex > openingIndex)
        //            {
        //                string potentialJson = source.Substring(openingIndex, closingIndex - openingIndex + 1);
        //                try
        //                {
        //                    JsonSerializer.Deserialize<object>(potentialJson);
        //                    json = potentialJson; // Set the out parameter
        //                    return true; // Return true, indicating a valid JSON string was found
        //                }
        //                catch (JsonException) { /* Not a valid JSON, continue search */ }
        //            }
        //        }
        //        openingIndex++;
        //    }
        //    return false; // Return false if no valid JSON string is found
        //}
    }
}
