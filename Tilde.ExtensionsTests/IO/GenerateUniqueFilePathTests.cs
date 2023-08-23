namespace Tilde.ExtensionsTests.IO;

[TestClass]
public class GenerateUniqueFilePathTests
{
    [TestMethod]
    public void Test_GenerateUniqueFilePath_NullOrEmptySource()
    {
        var result = StringExtensions.GenerateUniqueFilePath(null!);
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Source cannot be null or empty.", result.ErrorMessage);
    }

    [TestMethod]
    public void Test_GenerateUniqueFilePath_NotFullyQualifiedPath()
    {
        var result = StringExtensions.GenerateUniqueFilePath("filename.txt");
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Source must be a fully qualified path.", result.ErrorMessage);
    }

    [TestMethod]
    public void Test_GenerateUniqueFilePath_EnforceVersioning_Default()
    {
        var source = @"C:\path\to\file.txt"; // Make sure this path does not exist
        var result = StringExtensions.GenerateUniqueFilePath(source, enforceVersioning: true);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(@"C:\path\to\file (v0).txt", result.FilePath);
    }

    [TestMethod]
    public void Test_GenerateUniqueFilePath_NoEnforceVersioning_FileNotExist()
    {
        var source = @"C:\path\to\file.txt"; // Make sure this path does not exist
        var result = StringExtensions.GenerateUniqueFilePath(source, enforceVersioning: false);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(source, result.FilePath);
    }

    [TestMethod]
    public void Test_GenerateUniqueFilePath_CustomVersionToken()
    {
        var source = @"C:\path\to\file.txt"; // Make sure this path does not exist
        var result = StringExtensions.GenerateUniqueFilePath(source, enforceVersioning: true, versionToken: "version");
        Assert.IsTrue(result.Success);
        Assert.AreEqual(@"C:\path\to\file (version0).txt", result.FilePath);
    }

    [TestMethod]
    public void Test_GenerateUniqueFilePath_CustomSeparator()
    {
        var source = @"C:\path\to\file.txt"; // Make sure this path does not exist
        var result = StringExtensions.GenerateUniqueFilePath(source, enforceVersioning: true, separator: "_");
        Assert.IsTrue(result.Success);
        Assert.AreEqual(@"C:\path\to\file_(v0).txt", result.FilePath);
    }
}
