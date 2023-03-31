using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;

namespace Tilde.Extensions.Types
{
   public static partial class StringExtensions
   {
      public static string ReturnValidFilePath([DisallowNull] this string source, string versioning = Versioning.Default)
      {
         try
         {
            var path = Path.GetDirectoryName(source) ?? string.Empty;
            var file = Path.GetFileNameWithoutExtension(source);
            var ext = Path.GetExtension(source);
            switch (versioning)
            {
               default:
               case Versioning.Default:
                  var version = 0;
                  var match = Regex.Match(source, @"(.+) \(v(\d+)\)\.\w+");
                  if (match.Success)
                  {
                     file = match.Groups[1].Value;
                     version = int.Parse(match.Groups[2].Value);
                  }

                  do
                  {
                     version++;
                     source = Path.Combine(path, string.Format($"{file} ({Versioning.Default}{version}){ext}"));
                  } while (File.Exists(source));

                  break;

            }

            return source;
         }
         catch (Exception)
         {
            return string.Empty;
         }
      }
   }

   public static class Versioning
   {
      public const string Default = "v";
   }
}