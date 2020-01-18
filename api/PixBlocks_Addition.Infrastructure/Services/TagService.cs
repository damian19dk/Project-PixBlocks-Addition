using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PixBlocks_Addition.Domain.Entities;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Mappers;
using PixBlocks_Addition.Infrastructure.ResourceModels;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public TagService(ITagRepository tagRepository, ILocalizationService localizationService, IAutoMapperConfig mapperConfig)
        {
            _tagRepository = tagRepository;
            _localizationService = localizationService;
            _mapper = mapperConfig.Mapper;
        }

        public async Task<IEnumerable<TagDto>> BrowseAsync(string name)
        {
            var tags = await _tagRepository.BrowseAsync(name, _localizationService.Language);
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public async Task CreateAsync(TagResource tag)
        {
            var tagExists = await _tagRepository.GetAsync(tag.Name, tag.Language);
            if (tagExists != null)
            {
                throw new MyException(MyCodesNumbers.TagExists, $"The tag {tag.Name} already exists.");
            }

            await _tagRepository.AddAsync(new Tag(tag.Name, tag.Description, tag.Color, tag.Language));
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
        {
            var tags = await _tagRepository.GetAllAsync(_localizationService.Language);
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public async Task<TagDto> GetAsync(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);
            return _mapper.Map<TagDto>(tag);
        }

        public async Task RemoveAsync(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);
            if (tag == null)
            {
                throw new MyException(MyCodesNumbers.TagNotFound, "The tag does not exist.");
            }

            await _tagRepository.RemoveAsync(tag);
        }

        public async Task UpdateAsync(Guid id, TagResource tag)
        {
            var tagEntity = await _tagRepository.GetAsync(id);
            if(tagEntity == null)
            {
                throw new MyException(MyCodesNumbers.TagNotFound, "The tag does not exist.");
            }
            var tagExists = await _tagRepository.GetAsync(tag.Name, tag.Language);
            if(tagExists != null)
            {
                throw new MyException(MyCodesNumbers.TagExists, $"The tag with name {tag.Name} already exists.");
            }
            tagEntity.SetName(tag.Name);
            tagEntity.SetDescription(tag.Description);
            tagEntity.SetColor(tag.Color);
            tagEntity.SetLanguage(tag.Language);

            await _tagRepository.UpdateAsync(tagEntity);
        }
    }
}
