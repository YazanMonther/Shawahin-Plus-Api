using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ChargingStationsDto
{
    public class ChargerStationsInfoBase
    {
        public string? ParkingType { get; set; }

        public TimeSpan SundayStartTime { get; set; }
        public TimeSpan SundayEndTime { get; set; }

        public TimeSpan MondayStartTime { get; set; }
        public TimeSpan MondayEndTime { get; set; }

        public TimeSpan TuesdayStartTime { get; set; }
        public TimeSpan TuesdayEndTime { get; set; }

        public TimeSpan WednesdayStartTime { get; set; }
        public TimeSpan WednesdayEndTime { get; set; }

        public TimeSpan ThursdayStartTime { get; set; }
        public TimeSpan ThursdayEndTime { get; set; }

        public TimeSpan FridayStartTime { get; set; }
        public TimeSpan FridayEndTime { get; set; }

        public TimeSpan SaturdayStartTime { get; set; }
        public TimeSpan SaturdayEndTime { get; set; }
        public double? ChargerCost { get; set; }
        public Guid UserId { get; set; }

        public string? ChargerStatus { get; set; }


        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public double? Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180 degrees.")]
        public double? Longitude { get; set; }

        public string? BuildingNumber { get; set; }

        public string? StreetName { get; set; }

        public string? Town { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? ChargerType { get; set; }

        public string? PowerKw { get; set; }

        public string? ElectricType { get; set; }

        public string? StationName { get; set; }

        public string? ChargerImageUrl { get; set; }

        public string? ContactName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? PaymentMethod { get; set; }

        public string? PaymentType { get; set; }
    }
}
