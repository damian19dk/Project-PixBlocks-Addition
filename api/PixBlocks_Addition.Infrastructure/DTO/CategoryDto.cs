using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
    }
}
