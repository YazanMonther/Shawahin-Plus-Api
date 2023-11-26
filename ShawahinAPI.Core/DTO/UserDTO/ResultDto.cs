using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.UserDTO
{
   
    public class ResultDto
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
    }
}
