using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Tilde.Extensions;

public static partial class StringExtensions {
  public enum Versioning { Default, DateOnly, DateTime }

  /// <summary>
  /// Generates a unique file path based on the provided source path, applying optional versioning, separator, and version token customization.
  /// </summary>
  /// <param name="source">The original file path that needs to be made unique.</param>
  /// <param name="enforceVersioning">A flag to determine whether versioning should be applied even if the original file path is unique. Defaults to false.</param>
  /// <param name="versioning">The versioning strategy to apply. See <see cref="Versioning"/> enumeration for available strategies. Defaults to <see cref="Versioning.Default"/>.</param>
  /// <param name="separator">The separator character used between the file name and the versioning information. Defaults to a space (" ").</param>
  /// <param name="versionToken">The token to use for versioning, such as "v" for "version". Defaults to "v".</param>
  /// <returns>A tuple containing a success flag, the generated unique file path, and an error message if the operation failed.</returns>
  public static (bool Success, string FilePath, string ErrorMessage) GenerateUniqueFilePath(this string source, bool enforceVersioning = false, Versioning versioning = Versioning.Default, string separator = " ", string versionToken = "v") {
    if (string.IsNullOrEmpty(source))
      return (false, string.Empty, "Source cannot be null or empty.");
    if (!Path.IsPathFullyQualified(source))
      return (false, string.Empty, "Source must be a fully qualified path.");

    try {
      if (!enforceVersioning && !File.Exists(source))
        return (true, source, string.Empty);

      var path = Path.GetDirectoryName(source) ?? string.Empty;
      var file = Path.GetFileNameWithoutExtension(source);
      var ext = Path.GetExtension(source);

      int version = 0;
      string uniquePath;

      switch (versioning) {
        case Versioning.DateOnly:
          var dateVersion = DateTime.Now.ToString("yyyyMMdd");
          do {
            uniquePath = Path.Combine(path, $"{file}_{dateVersion}{(version > 0 ? "_" + version.ToString() : string.Empty)}{ext}");
            version++;
          } while (File.Exists(uniquePath));
          break;

        case Versioning.DateTime:
          var dateTimeVersion = DateTime.Now.ToString("yyyyMMdd_HHmmss");
          do {
            uniquePath = Path.Combine(path, $"{file}_{dateTimeVersion}{(version > 0 ? "_" + version.ToString() : string.Empty)}{ext}");
            version++;
          } while (File.Exists(uniquePath));
          break;

        default:
          var regexPattern = $@"(.+){Regex.Escape(separator)}\({versionToken}(\d+)\)\.\w+";
          var regex = new Regex(regexPattern, RegexOptions.Compiled);
          var match = regex.Match(file);
          if (match.Success) {
            file = match.Groups[1].Value; // Extract the file name without versioning
            version = int.Parse(match.Groups[2].Value); // Extract the current version number
          }

          do {
            uniquePath = Path.Combine(path, $"{file}{separator}({versionToken}{version}){ext}");
            version++;
          } while (File.Exists(uniquePath));
          break;
      }

      return (true, uniquePath, string.Empty);
    } catch (Exception e) {
      return (false, string.Empty, e.Message);
    }
  }
}