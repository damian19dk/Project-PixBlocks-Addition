using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class ImageDto
    {
        public string ContentType { get; set; }
        public byte[] Image { get; set; }
    }
}
