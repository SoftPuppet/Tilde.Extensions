using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Tilde.Extensions.Types
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Checks if the given string is a valid file path.
        /// </summary>
        /// <param name="source">The file path to validate.</param>
        /// <param name="reason">The reason why the file path is not valid, or null if the file path is valid.</param>
        /// <returns>True if the file path is valid, false otherwise.</returns>
        public static bool IsValidFilePath([DisallowNull] this string source, out string reason)
        {
            // Return false if the path is null or empty
            if (string.IsNullOrEmpty(source) || string.IsNullOrWhiteSpace(source))
            {
                reason = "The provided string is empty.";
                return false;
            }

            // Validate the path using Path.GetFullPath
            try
            {
                string fullPath = Path.GetFullPath(source);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is NotSupportedException || ex is PathTooLongException)
            {
                reason = ex.Message; // You could provide custom messages based on the exception type
                return false;
            }

            // Check for invalid file name characters
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            if (Path.GetFileName(source).IndexOfAny(invalidFileNameChars) >= 0)
            {
                reason = "The file name contains invalid characters.";
                return false;
            }

            // The file path and file name are valid
            reason = string.Empty;
            return true;
        }
    }
}
