using SoundCloudExplode;
using WaveSound.Common.Exceptions;
using WaveSound.Common.Extensions;
using WaveSound.Domain.Models;
using WaveSound.Domain.Services.Interfaces;

namespace WaveSound.Domain.Services
{
    public class SoundCloudService : ISoundCloudService
    {
        private readonly IDownloadPathUpdater _pathUpdater;

        public SoundCloudService(IDownloadPathUpdater pathUpdater)
        {
            _pathUpdater = pathUpdater;
        }

        public async Task<SoundCloudDomainModel> ConvertSoundCloudTrackAsync(string trackUrl)
        {
            var soundcloud = new SoundCloudClient();

            if (await soundcloud.Tracks.IsUrlValidAsync(trackUrl))
            {
                var track = await soundcloud.Tracks.GetAsync(trackUrl) ?? throw new TrackIsNullException();
                var trackName = PathEx.EscapeFileName(track.Title!);
                var downloadPath = await _pathUpdater.GetDownloadPathAsync();
                var trackPath = Path.Join(downloadPath, $"{trackName}.mp3");

                await soundcloud.DownloadAsync(track, trackPath);

                return new SoundCloudDomainModel { SuccessMessage = $"Conversion succeeded. Saved to: " + trackPath };
            }

            return new SoundCloudDomainModel { SuccessMessage = "Conversion failed. Make sure that the URL of the SoundCloud track is valid and not restricted to only premium users." };
        }
    }
}
