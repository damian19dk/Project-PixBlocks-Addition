using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.ResourceModels
{
    public class OrderVideosResource
    {
        public IEnumerable<Guid> Videos { get; set; }
        public Guid CourseId { get; set; }
    }
}
