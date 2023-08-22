using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Utilities;

namespace Tilde.ExtensionsTests.Utilities;

[TestClass]
public class EncryptionHelperTests
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private byte[] _testData;
    private KeySize _keySize;
    private byte[] _invalidKey;
    private byte[] _invalidIV;
    private byte[] _invalidEncryptedData;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [TestInitialize]
    public void Setup()
    {
        _testData = Encoding.UTF8.GetBytes("Test message");
        _keySize = KeySize.Key256bits;
        _invalidKey = new byte[8]; // Invalid key size
        _invalidIV = new byte[8];  // Invalid IV size
        _invalidEncryptedData = new byte[] { 1, 2, 3, 4, 5 };
    }

    [TestMethod]
    public void EncryptAes_ShouldFailWithNullData()
    {
        var result = EncryptionHelper.EncryptAes(null!, _keySize);
        Assert.IsFalse(result.IsSuccessful);
        Assert.AreEqual("Data to encrypt cannot be null or empty.", result.ErrorMessage);
    }

    [TestMethod]
    public void EncryptAes_ShouldFailWithEmptyData()
    {
        var encryptionResult = EncryptionHelper.EncryptAes(Array.Empty<byte>());
        Assert.IsFalse(encryptionResult.IsSuccessful);
        Assert.AreEqual(encryptionResult.ErrorMessage, "Data to encrypt cannot be null or empty.");
    }

    [TestMethod]
    public void EncryptAes_ShouldSucceed()
    {
        var encryptionResult = EncryptionHelper.EncryptAes(_testData);
        Assert.IsTrue(encryptionResult.IsSuccessful);
        Assert.IsNotNull(encryptionResult.EncryptedData);
        Assert.IsNotNull(encryptionResult.Key);
        Assert.IsNotNull(encryptionResult.IV);
        Assert.AreEqual(encryptionResult.ErrorMessage, string.Empty);
    }

    [TestMethod]
    public void DecryptAes_ShouldDecryptDataSuccessfully()
    {
        var encryptionResult = EncryptionHelper.EncryptAes(_testData, _keySize);
        var decryptionResult = EncryptionHelper.DecryptAes(encryptionResult.EncryptedData, encryptionResult.Key, encryptionResult.IV, encryptionResult.KeySize);

        Assert.IsTrue(decryptionResult.IsSuccessful);
        Assert.AreEqual(Encoding.UTF8.GetString(_testData), Encoding.UTF8.GetString(decryptionResult.DecryptedData));
        Assert.AreEqual(string.Empty, decryptionResult.ErrorMessage);
    }

    [TestMethod]
    public void DecryptAes_ShouldFailWithInvalidEncryptedData()
    {
        var encryptionResult = EncryptionHelper.EncryptAes(_testData, _keySize);
        var decryptionResult = EncryptionHelper.DecryptAes(_invalidEncryptedData, encryptionResult.Key, encryptionResult.IV, _keySize);
        Assert.IsFalse(decryptionResult.IsSuccessful);
    }

    [TestMethod]
    public void DecryptAes_ShouldFailWithInvalidKeySize()
    {
        var encryptionResult = EncryptionHelper.EncryptAes(_testData, _keySize);
        var decryptionResult = EncryptionHelper.DecryptAes(encryptionResult.EncryptedData, new byte[1], encryptionResult.IV, encryptionResult.KeySize);

        Assert.IsFalse(decryptionResult.IsSuccessful);
    }

    [TestMethod]
    public void DecryptAes_ShouldFailWithInvalidIV()
    {
        var encryptionResult = EncryptionHelper.EncryptAes(_testData);
        var decryptionResult = EncryptionHelper.DecryptAes(encryptionResult.EncryptedData, encryptionResult.Key, _invalidIV, encryptionResult.KeySize);
        Assert.IsFalse(decryptionResult.IsSuccessful);
        Assert.AreEqual(decryptionResult.ErrorMessage, "IV must be a 16-byte array.");
    }

    [TestMethod]
    public void DecryptAes_ShouldFailWithEmptyData()
    {
        var decryptionResult = EncryptionHelper.DecryptAes(Array.Empty<byte>(), _invalidKey, _invalidIV, _keySize);
        Assert.IsFalse(decryptionResult.IsSuccessful);
        Assert.AreEqual(decryptionResult.ErrorMessage, "Encrypted data cannot be null or empty.");
    }

    [TestMethod]
    public void DecryptAes_ShouldFailWithNullData()
    {
        var decryptionResult = EncryptionHelper.DecryptAes(null!, _invalidKey, _invalidIV, _keySize);
        Assert.IsFalse(decryptionResult.IsSuccessful);
        Assert.AreEqual(decryptionResult.ErrorMessage, "Encrypted data cannot be null or empty.");
    }
}

