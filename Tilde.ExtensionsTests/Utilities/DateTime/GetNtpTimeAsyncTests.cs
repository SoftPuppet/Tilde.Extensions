using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tilde.Utilities;

namespace Tilde.ExtensionsTests.Utilities;

[TestClass]
public class GetNtpTimeAsyncTests
{
    [TestMethod]
    public async Task GetNtpTimeAsync_Successful_ReturnsValidDateTime()
    {
        // Arrange
        string ntpServer = "time.cloudflare.com";
        int ntpPort = 123;

        // Act
        var result = await DateTimeHelper.GetNtpTimeAsync(ntpServer, ntpPort);

        // Assert
        Assert.IsTrue(result.IsSuccessful);
        Assert.IsNotNull(result.DateTime);
        Assert.AreNotEqual(result.DateTime, new DateTime());
        Assert.AreEqual(result.ErrorMessage, string.Empty);
    }

    [TestMethod]
    public async Task GetNtpTimeAsync_InvalidServer_ReturnsErrorMessage()
    {
        // Arrange
        string ntpServer = "invalid.server_askdfjk.com";
        int ntpPort = 123;

        // Act
        var result = await DateTimeHelper.GetNtpTimeAsync(ntpServer, ntpPort);

        // Assert
        Assert.IsFalse(result.IsSuccessful);
        Assert.AreEqual(result.DateTime, new DateTime());
        Assert.IsNotNull(result.ErrorMessage);
    }
}
