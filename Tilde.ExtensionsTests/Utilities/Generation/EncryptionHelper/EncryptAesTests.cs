//using System.Text;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Tilde.Utilities;

//namespace Tilde.ExtensionsTests.Utilities
//{
//    [TestClass]
//    public class EncryptAesTests
//    {
//        [TestMethod]
//        public void Test_EncryptAes_NullData()
//        {
//            var result = EncryptionHelper.EncryptAes(null!);
//            Assert.IsFalse(result.IsSuccessful);
//            Assert.AreEqual("Data to encrypt cannot be null or empty.", result.ErrorMessage);
//        }

//        [TestMethod]
//        public void Test_EncryptAes_EmptyData()
//        {
//            var result = EncryptionHelper.EncryptAes(new byte[0]);
//            Assert.IsFalse(result.IsSuccessful);
//            Assert.AreEqual("Data to encrypt cannot be null or empty.", result.ErrorMessage);
//        }

//        [TestMethod]
//        public void Test_EncryptAes_ValidData()
//        {
//            byte[] dataToEncrypt = Encoding.UTF8.GetBytes("Test Data");
//            var result = EncryptionHelper.EncryptAes(dataToEncrypt);

//            Assert.IsTrue(result.IsSuccessful);
//            Assert.IsNotNull(result.EncryptedData);
//            Assert.IsNotNull(result.Key);
//            Assert.IsNotNull(result.IV);
//            Assert.AreEqual(string.Empty, result.ErrorMessage);
//        }
//    }
//}
