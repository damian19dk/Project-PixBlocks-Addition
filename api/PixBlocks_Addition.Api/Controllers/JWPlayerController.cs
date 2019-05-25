using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.Models.JWPlayer;
using PixBlocks_Addition.Infrastructure.Services;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class JWPlayerController : ControllerBase
    {
        private readonly IJWPlayerService _jwPlayer;

        public JWPlayerController(IJWPlayerService jwPlayer)
        {
            _jwPlayer = jwPlayer;
        }
        
        [HttpGet("playlist")]
        public async Task<JWPlayerMedia> GetPlaylist(string id)
        {
            return await _jwPlayer.GetPlaylistAsync(id);
        }
        
        [HttpGet("video")]
        [Authorize(Policy = "Premium")]
        public async Task<JWPlayerVideo> GetVideo(string id)
        {
            return await _jwPlayer.GetVideoAsync(id);
        }

        [HttpGet("create")]
        public async Task<string> CreateVideo()
        {
            return await _jwPlayer.CreateVideoAsync();
        }

    }
}