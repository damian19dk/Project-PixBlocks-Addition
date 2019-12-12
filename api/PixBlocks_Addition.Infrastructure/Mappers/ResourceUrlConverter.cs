using AutoMapper;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Mappers
{
    public class ResourceUrlConverter: IValueConverter<ICollection<string>, ICollection<string>>
    {
        private readonly HostOptions _settings;

        public ResourceUrlConverter(IOptions<HostOptions> options)
        {
            _settings = options.Value;
        }

        public ICollection<string> Convert(ICollection<string> sourceMember, ResolutionContext context)
        {
            ICollection<string> result = new List<string>();
            if (sourceMember == null)
                return null;
            foreach (var member in sourceMember)
            {
                if (member != null)
                {
                    if (member.StartsWith("http") || member.StartsWith("https"))
                        result.Add(member);
                    else
                    {
                        if (_settings.HostName.EndsWith('/'))
                            result.Add(_settings.HostName + _settings.ResourceEndpoint + '/' + member);
                        else
                            result.Add(_settings.HostName + '/' + _settings.ResourceEndpoint + '/' + member);
                    }
                }
            }
            return result;
        }
    }
}
