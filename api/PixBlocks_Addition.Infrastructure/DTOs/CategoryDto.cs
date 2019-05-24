using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public CourseDto course { get; set; }
        public string Name { get; set; }
    }
}
