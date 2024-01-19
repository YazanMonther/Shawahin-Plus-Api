using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities.ChargingStationsEntities
{
    public class Bookings
    {
        public Guid Id { get; set; }
        public Guid StationId { get; set; }
        public Guid UserId { get; set; }
        public string? Status { get; set; }
        public DateTime startBooking { get; set; }
        public DateTime endBooking { get; set; }

        [ForeignKey("StationId")]
        public ChargingStations? ChargingStations { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? user { get; set; }
    }
}
