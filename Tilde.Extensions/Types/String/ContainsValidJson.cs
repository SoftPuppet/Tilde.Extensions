using System.Text;
using System.Text.Json;

namespace Tilde.Extensions.Types
{
    public static partial class StringExtensions
    {
        public static bool ContainsValidJson(this string source, out string? json)
        {
            json = null; // Initialize the out parameter
            if (source == null)
            { return false; }
            int openingIndex = source.IndexOf('{');
            while (openingIndex != -1)
            {
                int closingIndex = openingIndex;
                int braceCount = 1;
                StringBuilder potentialJsonBuilder = new StringBuilder();
                potentialJsonBuilder.Append('{');
                for (int i = openingIndex + 1; i < source.Length && braceCount > 0; i++)
                {
                    char c = source[i];
                    potentialJsonBuilder.Append(c);
                    if (c == '{') braceCount++;
                    else if (c == '}') braceCount--;
                    if (braceCount == 0) closingIndex = i;
                }

                if (closingIndex > openingIndex)
                {
                    string potentialJson = potentialJsonBuilder.ToString();
                    try
                    {
                        var obj = JsonSerializer.Deserialize<object>(potentialJson);
                        json = potentialJson; // Set the out parameter
                        return true; // Return true, indicating a valid JSON string was found
                    }
                    catch (JsonException) { /* Not a valid JSON, continue search */ }
                }

                openingIndex = source.IndexOf('{', openingIndex + 1);
            }
            return false; // Return false, indicating no valid JSON string was found
        }
    }
}
