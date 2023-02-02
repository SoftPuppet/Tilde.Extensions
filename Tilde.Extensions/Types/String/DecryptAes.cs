using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;

namespace Tilde.Extensions.Types
{
   public static partial class StringExtensions
   {
      public static string DecryptAes([DisallowNull] this string cipherText, byte[] key, byte[] iv)
      {
         if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
         if (key == null || key.Length <= 0)
            throw new ArgumentNullException("Key");
         if (iv == null || iv.Length <= 0)
            throw new ArgumentNullException("IV");

         byte[] cipherByte = Convert.FromBase64String(cipherText);

         using Aes aesAlg = Aes.Create();
         aesAlg.Key = key;
         aesAlg.IV = iv;
         ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

         using MemoryStream ms = new MemoryStream(cipherByte);
         using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
         using StreamReader sr = new StreamReader(cs);
         return sr.ReadToEnd();
      }
   }
}
