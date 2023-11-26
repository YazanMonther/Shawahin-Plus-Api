using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.UserDTO
{
    public class UserProfileUpdateDto
    {
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
