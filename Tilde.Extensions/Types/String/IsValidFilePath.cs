using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Tilde.Extensions.Types
{
    public static partial class StringExtensions
    {
        public static bool IsValidFilePath([DisallowNull] this string source)
        {
            FileInfo fileInfo = null!;
            try
            {
                fileInfo = new FileInfo(source);
            }
            catch (ArgumentException) { }
            catch (PathTooLongException) { }
            catch (NotSupportedException) { }

            if (fileInfo is null)
            {
                // The file path is not valid.
                return false;
            }

            // Check for invalid file name characters.
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

            if (fileInfo.Name.IndexOfAny(invalidFileNameChars) >= 0)
            {
                // The file name is not valid.
                return false;
            }
            // The file path and file name are valid.
            return true;
        }
    }
}
