using Newtonsoft.Json;

namespace PixBlocks_Addition.Infrastructure.Models.JWPlayer
{
    public class JWPlayerStatus
    {
        [JsonProperty("Key")]
        public string MediaId { get; set; }
        public long Size { get; set; }
        public double Duration { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public long Views { get; set; }
        public string UploadSessionId { get; set; }
        public string MD5 { get; set; }
        public JWPlayerError Error { get; set; }
    }
}
