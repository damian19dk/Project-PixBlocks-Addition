using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.ResourceModels
{
    public class ChangeMediaResource: MediaResource
    {
        public Guid Id { get; set; }
    }
}
