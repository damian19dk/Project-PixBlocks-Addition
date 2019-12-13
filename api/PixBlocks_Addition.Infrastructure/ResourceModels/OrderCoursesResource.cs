using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.ResourceModels
{
    public class OrderCoursesResource
    {
        public IEnumerable<Guid> Courses { get; set; }
    }
}
