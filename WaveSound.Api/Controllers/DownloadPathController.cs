using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WaveSound.Domain.Services.Interfaces;

namespace WaveSound.Api.Controllers
{
    [ApiController]
    [Route("api/DownloadPath")]
    public class DownloadPathController : Controller
    {
        private readonly IDownloadPathUpdater _pathUpdater;

        public DownloadPathController(IDownloadPathUpdater pathUpdater)
        {
            _pathUpdater = pathUpdater;
        }

        [HttpGet("DownloadPath")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var currentDownloadPath = await _pathUpdater.GetDownloadPathAsync();

                return Ok($"Current saved download path: {currentDownloadPath}");
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("DownloadPath")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([Required, FromQuery] string newDownloadPath)
        {
            try
            {
                if (string.IsNullOrEmpty(newDownloadPath))
                {
                    return BadRequest("File path is empty.");
                }

                await _pathUpdater.UpdateDownloadPathAsync(newDownloadPath);

                return Ok($"File path has been updated successfully. New download path: {newDownloadPath}");
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
