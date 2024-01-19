using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using System.Xml.Linq;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Core.Entities
{
    public class ChargingStations : ChargingStationBase
    {
        [Key]
        public Guid Id { get; set; }

        public double? Rate { get; set; }

        public int FavoriteCount { get; set; } 

        public int UserUsedCount { get; set; }
      
        public int views { get; set; }

        public double TotalRevenue { get; set; } 

        public ICollection<Bookings>? bookings { get; set; }

    }
}
