using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("show")]
        [Authorize(Policy = "Premium")]
        public async Task<JWPlayerStatus> ShowVideo(string mediaId)
            => await _jwPlayer.ShowVideoAsync(mediaId);

        [Authorize(Roles = "Administrator")]
        [HttpGet("create")]
        public async Task<string> CreateVideo()
        {
            return await _jwPlayer.CreateVideoAsync();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete")]
        public async Task DeleteVideo(string mediaId)
        {
            await _jwPlayer.DeleteVideoAsync(mediaId);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("upload")]
        public async Task<string> UploadVideo()
        {
            var file = Request.HasFormContentType ? Request.Form.Files.FirstOrDefault() : null;
            return await _jwPlayer.UploadVideoAsync(file);
        }
    }
}