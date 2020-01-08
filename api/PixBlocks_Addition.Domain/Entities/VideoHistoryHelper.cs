using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class VideoHistoryHelper
    {
        public Guid Id { get; protected set; }
        public Video Video { get; protected set; }
        public long Time { get; protected set; }

        public VideoHistoryHelper(Video video, long time)
        {
            Id = Guid.NewGuid();
            Video = video;
            Time = time;
        }

        private VideoHistoryHelper()
        {

        }
    }
}
