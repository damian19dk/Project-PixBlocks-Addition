using Microsoft.AspNetCore.Http;
using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.ResourceModels
{
    public class MediaResource
    {
        public Guid ParentId { get; set; }
        public bool Premium { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public IFormFile Image { get; set; }
        public string Language { get; set; }
        public string Tags { get; set; }
    }
}
