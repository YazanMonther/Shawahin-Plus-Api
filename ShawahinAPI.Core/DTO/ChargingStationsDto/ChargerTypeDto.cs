using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class ChargerTypeDto
    {
        public string? ChargerType { get; set; }
        public string? ChargerLogoUrl { get; set; }
   
    }
}
