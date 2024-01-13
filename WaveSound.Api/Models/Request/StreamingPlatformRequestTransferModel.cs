using System.ComponentModel.DataAnnotations;

namespace WaveSound.Api.Models.Request
{
    public class StreamingPlatformRequestTransferModel
    {
        [Required]
        public string TrackUrl { get; set; }
    }
}
