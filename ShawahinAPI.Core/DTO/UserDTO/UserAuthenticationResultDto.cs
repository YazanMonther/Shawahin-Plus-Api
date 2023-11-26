using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.UserDTO
{
    public class UserAuthenticationResultDto
    {
        public string? AccessToken { get; set; }
        public UserProfileDto? UserProfile { get; set; }
    }
}
