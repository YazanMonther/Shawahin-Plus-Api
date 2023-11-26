using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Mappers.UserMappers
{
    public static class UserRegistrationMapper
    {
        public static ApplicationUser MapToUserAuthenticationResultDto(
            UserRegistrationDto registrationDto)
        {
            Random random = new Random();

            return new ApplicationUser
            {
                UserName = $"{registrationDto.Fname}_{registrationDto.Lname}_{random.Next(1000, 9999)}",
                Email = registrationDto.Email,
                Fname = registrationDto.Fname,
                Lname = registrationDto.Lname,
                PhoneNumber = registrationDto.PhoneNumber,
                UserRole = registrationDto.UserRole,
            };

        }
    }
}

