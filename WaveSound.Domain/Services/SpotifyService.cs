using SpotifyExplode;
using WaveSound.Domain.Models;
using WaveSound.Domain.Services.Interfaces;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace WaveSound.Domain.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IDownloadPathUpdater _pathUpdater;

        public SpotifyService(IDownloadPathUpdater pathUpdater)
        {
            _pathUpdater = pathUpdater;
        }

        public async Task<SpotifyDomainModel> ConvertSpotifyTrackAsync(string trackUrl)
        {
            var spotify = new SpotifyClient();
            var track = await spotify.Tracks.GetAsync(trackUrl);
            var savePath = await _pathUpdater.GetDownloadPathAsync();

            using (HttpClient client = new())
            {
                try
                {
                    var youtube = new YoutubeClient();
                    var youtubeId = spotify.Tracks.GetYoutubeIdAsync(track.Id).Result;

                    var streamManifest = await youtube.Videos.Streams.GetManifestAsync($"https://youtube.com/watch?v={youtubeId}");
                    var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                    await youtube.Videos.Streams.DownloadAsync(streamInfo, Path.Combine(savePath, $"{track.Artists[0].Name} - {track.Title}.mp3"));

                    return new SpotifyDomainModel
                    {
                        SuccessMessage =
                            $"Conversion succeeded. Saved to: {Path.Combine(savePath, $"{track.Artists[0].Name} - {track.Title}.mp3")}"
                    };
                }

                catch (Exception exception)
                {
                    return new SpotifyDomainModel { SuccessMessage = $"Conversion failed. Error {exception.Message}" };
                }
            }
        }
    }
}
