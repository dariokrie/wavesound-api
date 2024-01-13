using WaveSound.Domain.Models;

namespace WaveSound.Domain.Services.Interfaces
{
    public interface ISpotifyService
    {
        Task<SpotifyDomainModel> ConvertSpotifyTrackAsync(string trackUrl);
    }
}
