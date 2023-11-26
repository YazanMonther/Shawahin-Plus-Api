using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.UserDTO
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = "";

        [Required]
        public string? Fname { get; set; }

        [Required]
        public string? Lname { get; set; }

        public string? PhoneNumber { get; set; }
        public UserRole UserRole { get; set; }
    }
}
