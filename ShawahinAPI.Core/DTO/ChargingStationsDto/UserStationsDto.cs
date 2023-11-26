using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class UserStationsDto : ChargingStationDto
    {
        public int UserUsedCount { get; set; }

        public int views { get; set; }

        public double TotalRevenue { get; set; }
    }
}
