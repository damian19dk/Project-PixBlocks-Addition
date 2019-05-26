using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Mappers
{
    public interface IAutoMapperConfig
    {
        IMapper Mapper { get; }
    }
}
