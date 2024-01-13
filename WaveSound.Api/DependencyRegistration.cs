using WaveSound.Api.Profiles;
using WaveSound.Domain.Services;
using WaveSound.Domain.Services.Interfaces;

namespace WaveSound.Api
{
    public static class DependencyRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDownloadPathUpdater, DownloadPathUpdater>();
            services.AddScoped<ISoundCloudService, SoundCloudService>();
            services.AddScoped<ISpotifyService, SpotifyService>();

            services.AddAutoMapper(
                typeof(StreamingPlatformProfile));

            return services;
        }
    }
}
