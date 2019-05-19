using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Models.JWPlayer
{
    public class JWPlayerMedia
    {
        [JsonProperty("playlist")]
        public ICollection<JWPlayerVideo> Videos { get; set; }
        public string Title { get; set; }

        public JWPlayerMedia()
        {
            Videos = new HashSet<JWPlayerVideo>();
        }
    }
}
