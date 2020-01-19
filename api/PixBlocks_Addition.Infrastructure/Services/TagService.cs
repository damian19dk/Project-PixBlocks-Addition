using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
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
        private readonly ILocalizationService _localization;

        public TagService(ITagRepository tagRepository, ILocalizationService localizationService, IAutoMapperConfig mapperConfig,
            ILocalizationService localization)
        {
            _tagRepository = tagRepository;
            _localizationService = localizationService;
            _mapper = mapperConfig.Mapper;
            _localization = localization;
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
                if (_localization.Language == "en")
                    throw new MyException(MyCodesNumbers.TagExists, $"The tag {tag.Name} already exists!");
                else
                    throw new MyException(MyCodesNumbers.TagExists, $"Tag o nazwie: {tag.Name} już istnieje!");
            }

            await _tagRepository.AddAsync(new Tag(tag.Name, tag.Description, tag.FontColor, tag.BackgroundColor, tag.Language, _localization.Language));
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
        {
            var tags = await _tagRepository.GetAllAsync(_localizationService.Language);
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public async Task<TagDto> GetAsync(string name)
        {
            var tag = await _tagRepository.GetAsync(name, _localizationService.Language);
            return _mapper.Map<TagDto>(tag);
        }

        public async Task<TagDto> GetAsync(Guid id)
        {

            string file = $"Resources\\MyExceptions.{_localization.Language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            var tag = await _tagRepository.GetAsync(id);
            return _mapper.Map<TagDto>(tag);
        }

        public async Task RemoveAsync(Guid id)
        {
            string file = $"Resources\\MyExceptions.{_localization.Language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            var tag = await _tagRepository.GetAsync(id);
            if (tag == null)
            {
                throw new MyException(MyCodesNumbers.TagNotFound, doc.SelectSingleNode($"exceptions/TagNotFound").InnerText);
            }

            await _tagRepository.RemoveAsync(tag);
        }

        public async Task UpdateAsync(Guid id, TagResource tag)
        {
            string file = $"Resources\\MyExceptions.{_localization.Language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            var tagEntity = await _tagRepository.GetAsync(id);

            if (tagEntity == null)
            {
                throw new MyException(MyCodesNumbers.TagNotFound, doc.SelectSingleNode($"exceptions/TagNotFound").InnerText);
            }
            var tagExists = await _tagRepository.GetAsync(tag.Name, tag.Language);
            if (tag.Name != tagEntity.Name)
            {
                if (tagExists != null)
                {
                    if (_localization.Language == "en")
                        throw new MyException(MyCodesNumbers.TagExists, $"The tag {tag.Name} already exists!");
                    else
                        throw new MyException(MyCodesNumbers.TagExists, $"Tag o nazwie: {tag.Name} już istnieje!");
                }
            }
            tagEntity.SetName(tag.Name, _localization.Language);
            tagEntity.SetDescription(tag.Description, _localization.Language);
            tagEntity.SetColor(tag.FontColor, tag.BackgroundColor, _localization.Language);
            tagEntity.SetLanguage(tag.Language, _localization.Language);

            await _tagRepository.UpdateAsync(tagEntity);
        }
    }
}
