using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace PixBlocks_Addition.Api.ExceptionMessages
{
    public static class ExceptionMessages
    {
        public static void LoadMessagesToCache(IMemoryCache cache, IConfiguration configuration)
        {
            var errorMessageSection = configuration.GetSection("exceptionMessages");
            foreach (var language in errorMessageSection.GetChildren())
            {
                foreach (var error in language.GetChildren())
                {
                    var key = error.Key + "-" + language.Key;
                    cache.Set<string>(key, error.Value, new MemoryCacheEntryOptions() { Priority = CacheItemPriority.NeverRemove });
                }
            }
        }
    }
}
