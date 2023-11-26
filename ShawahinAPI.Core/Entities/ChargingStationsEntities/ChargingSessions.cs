using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities
{
    public class ChargingSessions
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ChargingStation")]
        public Guid ChargingStationId { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EnergyConsumed { get; set; }
        public int Cost { get; set; }

        public ChargingStations? ChargingStation { get; set; }
        public ApplicationUser? Customer { get; set; }

    }
}
