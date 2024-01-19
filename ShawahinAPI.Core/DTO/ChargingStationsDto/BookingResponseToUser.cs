using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class BookingResponseToUser : BookingResponseDto
    {
        public string? OwnerContatPhone {get;set;}
        
        public string? OwnerName { get; set; }
    }
}
