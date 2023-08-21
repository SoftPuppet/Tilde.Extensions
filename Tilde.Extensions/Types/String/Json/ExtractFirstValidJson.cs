/*
// Author: Pauly (@SoftPuppet)
// ExtraFirstValidJson.cs
// File contains two extension methods/overrides
// public string ExtractFirstValidJson(this string source)
// public static T ExtractFirstValidJson<T>(this string source) 
*/
using System.Text.Json;

namespace Tilde.Extensions.Types
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Determines if the source string contains any valid JSON object, and returns the first json string found.
        /// </summary>
        /// <param name="source">The string to be scanned for valid JSON objects.</param>
        /// <returns>First json string found in the source.</returns>
        public static string ExtractFirstValidJson(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
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
                                return jsonSlice;
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
            return string.Empty;
        }

        // <summary>
        /// Extracts the first valid JSON object from the source string and deserializes it to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="source">The source string to search for valid JSON.</param>
        /// <returns>An instance of the specified type if a valid JSON object is found; otherwise, the default value of the type.</returns>
        public static T ExtractFirstValidJson<T>(this string source)
        {
#pragma warning disable CS8603 // Possible null reference return.
            if (string.IsNullOrEmpty(source))
            {
                return default;
            }
            var jsonString = source.ExtractFirstValidJson();
            if (string.IsNullOrEmpty(jsonString))
            {
                return default;
            }
            try
            {
                T deserializedObject = JsonSerializer.Deserialize<T>(jsonString);
                return deserializedObject;
            }
            catch (JsonException)
            {
                return default;
            }
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
