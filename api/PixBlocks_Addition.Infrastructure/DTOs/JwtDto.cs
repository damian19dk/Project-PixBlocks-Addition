using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class JwtDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expires { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
    }
}
