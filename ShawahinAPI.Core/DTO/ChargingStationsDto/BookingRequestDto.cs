using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class BookingRequestDto
    {
        public Guid StationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartBooking { get; set; }
        public DateTime EndBooking { get; set; }
    }
}
