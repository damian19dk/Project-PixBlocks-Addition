using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class VideoHistory
    {
        public Guid Id { get; protected set; }
        public User User { get; protected set; }
        public ICollection<VideoRecord> Videos { get; protected set; } = new HashSet<VideoRecord>();

        public VideoHistory(User user)
        {
            Id = Guid.NewGuid();
            User = user;
        }

        protected VideoHistory()
        {

        }
    }
}
