using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WaveSound.Api.Models.Request;
using WaveSound.Api.Models.Response;
using WaveSound.Domain.Models;
using WaveSound.Domain.Services.Interfaces;

namespace WaveSound.Api.Controllers
{
    [ApiController]
    [Route("api/SoundCloud")]
    public class SoundCloudController : Controller
    {
        private readonly ISoundCloudService _soundCloudService;
        private readonly IMapper _mapper;

        public SoundCloudController(ISoundCloudService soundCloudService, IMapper mapper)
        {
            _soundCloudService = soundCloudService;
            _mapper = mapper;
        }

        [HttpPost("Download")]
        [ProducesResponseType(typeof(StreamingPlatformResponseTransferModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromQuery] StreamingPlatformRequestTransferModel request)
        {
            try
            {
                var soundCloudDomainModel = await _soundCloudService.ConvertSoundCloudTrackAsync(request.TrackUrl);
                var soundCloudResponseTransferModel =
                    _mapper.Map<SoundCloudDomainModel, StreamingPlatformResponseTransferModel>(soundCloudDomainModel);

                return Ok(soundCloudResponseTransferModel);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
