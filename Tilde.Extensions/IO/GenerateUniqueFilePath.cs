using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Tilde.Extensions.IO
{
    public static partial class StringExtensions
    {
        public enum Versioning { Default, DateOnly, DateTime }

        public static (bool Success, string FilePath, string ErrorMessage) GenerateUniqueFilePath(this string source, bool enforceVersioning = false, Versioning versioning = Versioning.Default, string separator = " ", string versionToken = "v")
        {
            if (string.IsNullOrEmpty(source))
                return (false, string.Empty, "Source cannot be null or empty.");
            if (!Path.IsPathFullyQualified(source))
                return (false, string.Empty, "Source must be a fully qualified path.");

            try
            {
                if (!enforceVersioning && !File.Exists(source))
                    return (true, source, string.Empty);

                var path = Path.GetDirectoryName(source) ?? string.Empty;
                var file = Path.GetFileNameWithoutExtension(source);
                var ext = Path.GetExtension(source);

                int version = 0;
                string uniquePath;

                switch (versioning)
                {
                    case Versioning.DateOnly:
                        var dateVersion = DateTime.Now.ToString("yyyyMMdd");
                        do
                        {
                            uniquePath = Path.Combine(path, $"{file}_{dateVersion}{(version > 0 ? "_" + version.ToString() : string.Empty)}{ext}");
                            version++;
                        } while (File.Exists(uniquePath));
                        break;

                    case Versioning.DateTime:
                        var dateTimeVersion = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        do
                        {
                            uniquePath = Path.Combine(path, $"{file}_{dateTimeVersion}{(version > 0 ? "_" + version.ToString() : string.Empty)}{ext}");
                            version++;
                        } while (File.Exists(uniquePath));
                        break;

                    default:
                        var regexPattern = $@"(.+){Regex.Escape(separator)}\({versionToken}(\d+)\)\.\w+";
                        var regex = new Regex(regexPattern, RegexOptions.Compiled);
                        var match = regex.Match(file);
                        if (match.Success)
                        {
                            file = match.Groups[1].Value; // Extract the file name without versioning
                            version = int.Parse(match.Groups[2].Value); // Extract the current version number
                        }

                        do
                        {
                            uniquePath = Path.Combine(path, $"{file}{separator}({versionToken}{version}){ext}");
                            version++;
                        } while (File.Exists(uniquePath));
                        break;
                }

                return (true, uniquePath, string.Empty);
            }
            catch (Exception e)
            {
                return (false, string.Empty, e.Message);
            }
        }
    }
}