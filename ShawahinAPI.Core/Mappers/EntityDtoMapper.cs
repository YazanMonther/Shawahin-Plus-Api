using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Mappers
{

    public static class EntityDtoMapper<TEntity, TDto>
    {
        private static IMapper _mapper;

        static EntityDtoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntity, TDto>();
                cfg.CreateMap<TDto, TEntity>();
            });

            _mapper = config.CreateMapper();
        }

        public static TDto MapToDto(TEntity? entity)
        {
            return _mapper.Map<TDto>(entity);
        }

        public static IEnumerable<TDto> MapToDto(IEnumerable<TEntity>? entities)
        {
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public static TEntity MapToEntity(TDto? dto)
        {
            return _mapper.Map<TEntity>(dto);
        }

        public static IEnumerable<TEntity> MapToEntity(IEnumerable<TDto>? dtos)
        {
            return _mapper.Map<IEnumerable<TEntity>>(dtos);
        }

        public static void MapToEntity(TDto? dto, TEntity? entity)
        {
            _mapper.Map(dto, entity);
        }
    }
}
