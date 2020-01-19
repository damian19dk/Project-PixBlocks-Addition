using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.ResourceModels
{
    public class UploadResource
    {
        public Guid ParentId { get; set; }
        public string MediaId { get; set; }
    }
}
