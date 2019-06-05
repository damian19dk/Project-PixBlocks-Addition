using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class MediaDto
    {
        public Guid Id { get; set; }
        public string MediaId { get; set; }
        public string Category { get; set; }
        public bool Premium { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public long Duration { get; set; }
        public DateTime PublishDate { get; set; }
        public string Language { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}
