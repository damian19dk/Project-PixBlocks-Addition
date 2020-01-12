using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using PixBlocks_Addition.Infrastructure.Services;

namespace PixBlocks_Addition.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task CreateAsync([FromBody]TagResource tag)
            => await _tagService.CreateAsync(tag);

        [HttpGet]
        public async Task<IEnumerable<TagDto>> GetAllAsync()
            => await _tagService.GetAllAsync();

        [HttpGet("{name}")]
        public async Task<TagDto> GetAsync(string name)
            => await _tagService.GetAsync(name);

        [HttpGet("browse")]
        public async Task<IEnumerable<TagDto>> BrowseAsync(string name)
            => await _tagService.BrowseAsync(name);

        [Authorize(Roles = "Administrator")]
        [HttpPut("{name}")]
        public async Task UpdateAsync(string name, [FromBody]TagResource tag)
            => await _tagService.UpdateAsync(name, tag);

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{name}")]
        public async Task RemoveAsync(string name)
            => await _tagService.RemoveAsync(name);
    }
}