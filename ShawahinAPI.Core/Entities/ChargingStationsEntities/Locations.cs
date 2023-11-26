using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Spatial;
using static System.Collections.Specialized.BitVector32;

namespace ShawahinAPI.Core.Entities
{
    public class Locations
    {
        [Key]
        public Guid Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? BuildingNumber { get; set; }
        public string? StreetName { get; set; }
        public string? Town { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public ICollection<ChargingStationRequests>? ChargingStationRequests { get; set; }
        public ICollection<ChargingStations>? ChargingStations { get; set; }

    }
}
