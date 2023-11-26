using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.UserDTO
{
    public class AuthUserDto
    {
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? PhoneNumber { get; set; }
        public UserRole UserRole { get; set; }
    }
}
