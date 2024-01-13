using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WaveSound.Common.Exceptions;
using WaveSound.Domain.Services.Interfaces;
using WaveSound.Domain.Services;

namespace WaveSound.UnitTest
{
    [TestClass]
    public class SoundCloudServiceTests
    {
        [TestMethod]
        public async Task ConvertSoundCloudTrackAsync_ValidUrl_ShouldReturnSuccessMessage()
        {
            // Arrange
            var trackUrl = "https://soundcloud.com/kamron-tidjane/vision-palace?utm_source=clipboard&utm_medium=text&utm_campaign=social_sharing";
            var pathUpdaterMock = new Mock<IDownloadPathUpdater>();
            pathUpdaterMock.Setup(x => x.GetDownloadPathAsync()).ReturnsAsync("C:\\Downloads");

            var soundCloudService = new SoundCloudService(pathUpdaterMock.Object);

            // Act
            var result = await soundCloudService.ConvertSoundCloudTrackAsync(trackUrl);

            // Assert
            Assert.IsTrue(result.SuccessMessage.Contains("Conversion succeeded"));
        }

        [TestMethod]
        public async Task ConvertSoundCloudTrackAsync_InvalidUrl_ShouldReturnFailureMessage()
        {
            // Arrange
            var invalidUrl = "https://soundcloud.com/invalidtrack";
            var pathUpdaterMock = new Mock<IDownloadPathUpdater>();
            pathUpdaterMock.Setup(x => x.GetDownloadPathAsync()).ReturnsAsync("C:\\Downloads");

            var soundCloudService = new SoundCloudService(pathUpdaterMock.Object);

            // Act
            var result = await soundCloudService.ConvertSoundCloudTrackAsync(invalidUrl);

            // Assert
            Assert.IsTrue(result.SuccessMessage.Contains("Conversion failed"));
        }
    }
}
