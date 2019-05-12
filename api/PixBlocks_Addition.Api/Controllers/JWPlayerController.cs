using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.Models;
using PixBlocks_Addition.Infrastructure.Models.JWPlayer;
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
        public async Task<Media> GetPlaylist(string id)
        {
            return await _jwPlayer.GetPlaylistAsync(id);
        }
        
        [HttpGet("video")]
        public async Task<Video> GetVideo(string id)
        {
            return await _jwPlayer.GetVideoAsync(id);
        }
    }
}