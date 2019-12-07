using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PixBlocks_Addition.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Mappers
{
    public class PictureUrlConverter : IValueConverter<string, string>
    {
        private readonly HostOptions _settings;

        public PictureUrlConverter(IOptions<HostOptions> options)
        {
            _settings = options.Value;
        }
        
        public string Convert(string sourceMember, ResolutionContext context)
        {
            if (sourceMember == null)
                return null;
            if (sourceMember.StartsWith("http") || sourceMember.StartsWith("https"))
                return sourceMember;
            else
            {
                if (_settings.HostName.EndsWith('/'))
                    return _settings.HostName + _settings.ResourceEndpoint + '/' + sourceMember;
                else
                    return _settings.HostName + '/' + _settings.ResourceEndpoint + '/' + sourceMember;
            }
        }
    }
}
