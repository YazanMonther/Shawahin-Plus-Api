using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ShawahinAPI.Core.Entities.ChargingStationsEntities
{
    public class Contacts
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<ChargingStationRequests>? ChargingStationRequests { get; set; }
        public ICollection<ChargingStations>? ChargingStations { get; set; }

    }
}
