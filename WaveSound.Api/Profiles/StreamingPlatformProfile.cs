using AutoMapper;
using WaveSound.Api.Models.Response;
using WaveSound.Domain.Models;

namespace WaveSound.Api.Profiles
{
    public class StreamingPlatformProfile : Profile
    {
        public StreamingPlatformProfile()
        {
            CreateMap<SpotifyDomainModel, StreamingPlatformResponseTransferModel>()
                .ForMember(
                    dest => dest.SuccessMessage,
                    opt => opt.MapFrom(src => src.SuccessMessage));

            CreateMap<SoundCloudDomainModel, StreamingPlatformResponseTransferModel>()
                .ForMember(
                    dest => dest.SuccessMessage,
                    opt => opt.MapFrom(src => src.SuccessMessage));
        }
    }
}
