namespace WaveSound.Domain.Services.Interfaces
{
    public interface IDownloadPathUpdater
    {
        Task UpdateDownloadPathAsync(string newDownloadPath);
        Task<string> GetDownloadPathAsync();
    }
}
