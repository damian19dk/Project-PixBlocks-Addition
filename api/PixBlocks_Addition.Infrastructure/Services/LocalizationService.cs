using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PixBlocks_Addition.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Domain.Repositories;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IReadOnlyList<string> SupportedLanguages { get; }
        public string Language { get; }

        public LocalizationService(IHttpContextAccessor httpContextAccessor, IOptions<LanguageOptions> options)
        {
            _httpContextAccessor = httpContextAccessor;
            SupportedLanguages = options.Value.Languages.AsReadOnly();
            var headerLanguage = getCurrentLanguage().ToLowerInvariant();
            Language = IsSupportedLanguage(headerLanguage) ? headerLanguage : "en";
        }

        public bool IsSupportedLanguage(string language)
            => SupportedLanguages.Contains(language);


        private string getCurrentLanguage()
        {
            if (getLanguages().Any(lang => SupportedLanguages.Contains(lang.ToLowerInvariant())))
                return getLanguages().FirstOrDefault(lang => SupportedLanguages.Contains(lang.ToLowerInvariant()));
            else
                return getLanguages().FirstOrDefault();
        }

        private string[] getLanguages()
            => _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString()
               .Split(',', '-');
    }
}
