using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class VideoDto: MediaDto
    {
        public string Status { get; set; }
        public Guid ParentId { get; set; }
    }
}
