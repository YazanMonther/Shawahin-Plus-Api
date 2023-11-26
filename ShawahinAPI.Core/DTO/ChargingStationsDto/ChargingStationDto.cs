using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class ChargingStationDto :  ChargerStationsInfoBase
    {
        public Guid? StationId { get; set; }

        public string? ChargerName { get; set; }

        public double? Rate { get; set; }
        public string? CurrentChargerStatus { get; set; }
    
        public int  NumOfFavorites { get; set; }

    }
}
