using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Video: Media
    {
        public Video(string mediaId, bool premium, string title, string description, string picture, long duration,
            string lang, IEnumerable<Tag> tags = null)
            : base(mediaId, premium, title, description, picture, duration, lang, tags)
        {
        }

        protected Video()
        {
        }
    }
}
