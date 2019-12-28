using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class UserCourseHistoryDto
    {
        public Guid Id { get; set; }
        public CourseDto course { get; set; }
    }
}
