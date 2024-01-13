using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WaveSound.Domain.Services.Interfaces;
using WaveSound.Domain.Services;

namespace WaveSound.UnitTest
{
    [TestClass]
    public class SpotifyServiceTests
    {
        [TestMethod]
        public async Task ConvertSpotifyTrackAsync_ValidUrl_ShouldReturnSuccessMessage()
        {
            // Arrange
            var trackUrl = "https://open.spotify.com/track/2CaSJocFwU8ef2KB332fRi?si=68bb1f7f05f349e1";
            var pathUpdaterMock = new Mock<IDownloadPathUpdater>();
            pathUpdaterMock.Setup(x => x.GetDownloadPathAsync()).ReturnsAsync("C:\\Downloads");

            var spotifyService = new SpotifyService(pathUpdaterMock.Object);

            // Act
            var result = await spotifyService.ConvertSpotifyTrackAsync(trackUrl);

            // Assert
            Assert.IsTrue(result.SuccessMessage.Contains("Conversion succeeded"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ConvertSpotifyTrackAsync_InvalidUrl_ShouldReturnFailureMessage()
        {
            // Arrange
            var invalidUrl = "https://open.spotify.com/trackinvalid";
            var pathUpdaterMock = new Mock<IDownloadPathUpdater>();
            pathUpdaterMock.Setup(x => x.GetDownloadPathAsync()).ReturnsAsync("C:\\Downloads");

            var spotifyService = new SpotifyService(pathUpdaterMock.Object);

            // Act
            await spotifyService.ConvertSpotifyTrackAsync(invalidUrl);
        }
    }
}
