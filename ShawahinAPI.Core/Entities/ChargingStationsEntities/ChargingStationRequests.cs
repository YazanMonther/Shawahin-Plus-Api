using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Spatial;
using static System.Collections.Specialized.BitVector32;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using System.Runtime.InteropServices;
using ShawahinAPI.Core.Entities;

namespace ShawahinAPI.Core.Entities
{
    public class ChargingStationRequests : ChargingStationBase
    {
        [Key]
        public Guid Id { get; set; }
        public RequestStatus? Request_Status { get; set; }


    }

}
