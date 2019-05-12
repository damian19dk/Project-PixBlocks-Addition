using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Models.JWPlayer
{
    public class Media
    {
        [JsonProperty("playlist")]
        ICollection<Video> Videos { get; set; }
        public string Title { get; set; }

        public Media()
        {
            Videos = new HashSet<Video>();
        }
    }
}
