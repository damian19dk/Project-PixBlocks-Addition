using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Models
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}
