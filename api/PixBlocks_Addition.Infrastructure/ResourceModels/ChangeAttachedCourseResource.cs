using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.ResourceModels
{
    public class ChangeAttachedCourseResource
    {
        public Guid VideoId { get; set; }
        public Guid CourseId { get; set; }
    }
}
