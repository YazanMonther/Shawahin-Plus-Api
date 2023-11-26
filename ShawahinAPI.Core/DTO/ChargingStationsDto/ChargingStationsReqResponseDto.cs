using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class ChargingStationsReqResponseDto : AddChargingStationsReqDto
    {
        public Guid RequestId { get; set; }
        public string? RequestStatus { get; set; }

    }
}
