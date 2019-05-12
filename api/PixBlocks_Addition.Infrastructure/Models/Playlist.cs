using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Models
{
    public class Playlist
    {
        [JsonProperty("playlist")]
        ICollection<Video> Videos { get; set; }
        public string Title { get; set; }

        public Playlist()
        {
            Videos = new HashSet<Video>();
        }
    }
}
