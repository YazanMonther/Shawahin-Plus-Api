using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class BookingResponseDto
    {
        public Guid BookingId { get; set; }
        public string? ChargerName { get; set; }
        public DateTime StartBooking { get; set; }
        public DateTime EndBooking { get; set; }
        public string? Status { get; set; }

    }
}
