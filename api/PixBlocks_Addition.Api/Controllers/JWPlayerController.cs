using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.Models;
using PixBlocks_Addition.Infrastructure.Services;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWPlayerController : ControllerBase
    {
        private readonly IJWPlayerService _jwPlayer;

        public JWPlayerController(IJWPlayerService jwPlayer)
        {
            _jwPlayer = jwPlayer;
        }

        [AllowAnonymous]
        [HttpGet("playlist")]
        public async Task<Playlist> GetPlaylist(string id)
        {
            var token = getAuthorizationToken();
            return await _jwPlayer.GetPlaylistAsync(id, token);
        }
        
        [HttpGet("default")]
        public async Task<Playlist> Get()
        {
            var token = getAuthorizationToken();
            return await _jwPlayer.GetDefaultAsync(token);
        }

        private string getAuthorizationToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            token = token.Split(' ').Last();
            return token;
        }
    }
}