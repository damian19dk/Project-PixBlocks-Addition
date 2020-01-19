using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class HistoryDto
    {
        public Guid Id { get; set; }
        public UserDto user {get; set;}
        public CourseDto course { get; set; }
    }
}
