using System;
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

        [HttpGet("{id}")]
        public async Task<TagDto> GetAsync(Guid id)
            => await _tagService.GetAsync(id);

        [HttpGet("browse")]
        public async Task<IEnumerable<TagDto>> BrowseAsync(string name)
            => await _tagService.BrowseAsync(name);

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task UpdateAsync(Guid id, [FromBody]TagResource tag)
            => await _tagService.UpdateAsync(id, tag);

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task RemoveAsync(Guid id)
            => await _tagService.RemoveAsync(id);
    }
}