using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.ResourceModels
{
    public class ChangeResource
    {
        public Guid Id { get; set; }
        public IFormFile ResourceFile { get; set; }
    }
}