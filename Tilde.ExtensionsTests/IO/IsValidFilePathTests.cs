using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Extensions.IO;

namespace Tilde.ExtensionsTests.IO;

[TestClass]
public class IsValidFilePathTests
{
    [TestMethod]
    public void IsValidFilePath_ValidPath_ReturnsTrue()
    {
        string reason;
        bool result = IOExtensions.IsValidFilePath(@"C:\validpath\file.txt", out reason);
        Assert.IsTrue(result);
        Assert.AreEqual(string.Empty, reason);
    }

    [TestMethod]
    public void IsValidFilePath_NullString_ReturnsFalse()
    {
        string reason;
        bool result = IOExtensions.IsValidFilePath(null!, out reason);
        Assert.IsFalse(result);
        Assert.AreEqual("The provided string is empty.", reason);
    }

    [TestMethod]
    public void IsValidFilePath_EmptyString_ReturnsFalse()
    {
        string reason;
        bool result = IOExtensions.IsValidFilePath(string.Empty, out reason);
        Assert.IsFalse(result);
        Assert.AreEqual("The provided string is empty.", reason);
    }

    [TestMethod]
    public void IsValidFilePath_WhiteSpaceString_ReturnsFalse()
    {
        string reason;
        bool result = IOExtensions.IsValidFilePath("    ", out reason);
        Assert.IsFalse(result);
        Assert.AreEqual("The provided string is empty.", reason);
    }

    //[TestMethod]
    //public void IsValidFilePath_PathTooLong_ReturnsFalse()
    //{
    //    string longPath = new string('a', 513); // Adjust this length based on the specific OS limitations
    //    string reason;
    //    bool result = StringExtensions.IsValidFilePath(longPath, out reason);
    //    Assert.IsFalse(result);
    //    StringAssert.StartsWith(reason, "The specified path, file name, or both are too long."); // Adjust this based on the exact exception message
    //}

    //[TestMethod]
    //public void IsValidFilePath_NotSupportedPathFormat_ReturnsFalse()
    //{
    //    string reason;
    //    bool result = StringExtensions.IsValidFilePath("C::\\invalidpath\\file.txt", out reason);
    //    Assert.IsFalse(result);
    //    StringAssert.StartsWith(reason, "The given path's format is not supported."); // Adjust this based on the exact exception message
    //}

    [TestMethod]
    public void IsValidFilePath_InvalidFileNameChars_ReturnsFalse()
    {
        string reason;
        bool result = IOExtensions.IsValidFilePath(@"C:\path\file<.txt", out reason); // '<' is usually an invalid character in file names
        Assert.IsFalse(result);
        Assert.AreEqual("The file name contains invalid characters.", reason);
    }
}
