using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WaveSound.Api.Models.Request;
using WaveSound.Api.Models.Response;
using WaveSound.Domain.Models;
using WaveSound.Domain.Services.Interfaces;

namespace WaveSound.Api.Controllers
{
    [Controller]
    [Route("api/Spotify")]
    public class SpotifyController : Controller
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IMapper _mapper;

        public SpotifyController(ISpotifyService spotifyService, IMapper mapper)
        {
            _spotifyService = spotifyService;
            _mapper = mapper;
        }

        [HttpPost("Download")]
        [ProducesResponseType(typeof(StreamingPlatformResponseTransferModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromQuery] StreamingPlatformRequestTransferModel request)
        {
            try
            {
                var spotifyDomainModel = await _spotifyService.ConvertSpotifyTrackAsync(request.TrackUrl);
                var spotifyResponseTransferModel =
                    _mapper.Map<SpotifyDomainModel, StreamingPlatformResponseTransferModel>(spotifyDomainModel);

                return Ok(spotifyResponseTransferModel);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
