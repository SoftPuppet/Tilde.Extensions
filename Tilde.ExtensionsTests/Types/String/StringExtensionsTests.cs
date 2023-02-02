using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tilde.Extensions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Tilde.Extensions.Types.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void EncryptAesTest()
        {
            string plainText = "Some data to encrypt";
            string plainText2 = "ILUSDHFIWUefbncikjsdbviuwALKJSDFHIULHlkuihfjklcvhlieuarh";
            using (Aes myAes = Aes.Create())
            {
                string cipherText = plainText.EncryptAes(myAes.Key, myAes.IV);
                string decipherText = cipherText.DecryptAes(myAes.Key, myAes.IV);
                Assert.AreEqual(plainText, decipherText);

                string cipherText2 = plainText2.EncryptAes(myAes.Key, myAes.IV);
                string decipherText2 = cipherText2.DecryptAes(myAes.Key, myAes.IV);
                Assert.AreEqual(plainText2, decipherText2);
            }
        }
    }
}