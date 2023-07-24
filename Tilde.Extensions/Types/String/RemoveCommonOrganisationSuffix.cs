namespace Tilde.Extensions.Types
{
   public static partial class StringExtensions
   {
      public static string RemoveCommonOrganisationSuffix(this string @this, string[]? suffixList = null)
      {
         if (string.IsNullOrEmpty(@this)) { return string.Empty; }
         suffixList ??= new string[] { " pty.", " pty", " ltd.", " ltd", " limited.", " limited", " inc.", " inc", " incorporated" };
         return @this.RemoveAllTrailingStrings(suffixList);
      }
   }
}
