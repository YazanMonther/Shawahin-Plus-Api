using AutoMapper;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Mappers.UserMappers
{
    public class UserMapper
    {
        private readonly IMapper _mapper;

        public UserMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserLoginDto, ApplicationUser>();
                // Add more mapping configurations as needed
            });

            _mapper = config.CreateMapper();
        }

        public ApplicationUser MapToModel(UserLoginDto loginDto)
        {
            return _mapper.Map<ApplicationUser>(loginDto);
        }


    }
}