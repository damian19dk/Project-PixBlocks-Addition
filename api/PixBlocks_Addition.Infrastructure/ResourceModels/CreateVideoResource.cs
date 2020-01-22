using Microsoft.AspNetCore.Http;

namespace PixBlocks_Addition.Infrastructure.ResourceModels
{
    public class CreateVideoResource : MediaResource
    {
        public IFormFile Video { get; set; }
        public string MediaId { get; set; }
        public long Duration { get; set; }
    }
}
