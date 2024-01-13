using WaveSound.Domain.Models;

namespace WaveSound.Domain.Services.Interfaces
{
    public interface ISoundCloudService
    {
        Task<SoundCloudDomainModel> ConvertSoundCloudTrackAsync(string trackUrl);
    }
}
