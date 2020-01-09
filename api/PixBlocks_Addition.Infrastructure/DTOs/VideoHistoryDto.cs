using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class VideoHistoryDto
    {
        public UserDto User { get; set; }
        public ICollection<VideoRecordDto> Videos { get; set; }
    }
}
