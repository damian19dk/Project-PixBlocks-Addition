using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Models.JWPlayer
{
    public class Video
    {
        public string MediaId { get; set; }
        public string Description { get; set; }
        public long PubDate { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string FeedId { get; set; }
        public ICollection<Source> Sources { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public string Link { get; set; }
        public int Duration { get; set; }
    }
}
