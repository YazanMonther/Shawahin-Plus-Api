using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Mappers.UserMappers
{
    public static class UserAuthenticationMapper
    {
        public static UserAuthenticationResultDto MapToUserAuthenticationResultDto(
            string? accessToken,
            ApplicationUser user)
        {
            return new UserAuthenticationResultDto
            {
                AccessToken = accessToken,
                UserProfile = new UserProfileDto
                {
                    UserId = user.Id,
                    UserName = user.Fname+" "+user.Lname,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ProfileImageUrl = user.ProfileImageUrl,
                }
            };
        }
    }
}
