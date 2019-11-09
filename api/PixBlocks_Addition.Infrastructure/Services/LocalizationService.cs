using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PixBlocks_Addition.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IReadOnlyList<string> supportedLanguages { get; }

        public LocalizationService(IHttpContextAccessor httpContextAccessor, IOptions<LanguageOptions> options)
        {
            _httpContextAccessor = httpContextAccessor;
            supportedLanguages = options.Value.Languages.AsReadOnly();
        }

        public string GetCurrentLanguage()
            => getLanguages().FirstOrDefault(x => supportedLanguages.Contains(x.ToLowerInvariant()));

        public bool IsSupportedLanguage() 
            => getLanguages().Any(x => supportedLanguages.Contains(x));

        private string[] getLanguages() 
            => _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString()
               .Split(',', '-');
    }
}
