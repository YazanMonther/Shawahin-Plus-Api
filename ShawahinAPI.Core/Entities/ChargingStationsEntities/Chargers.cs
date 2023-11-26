using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShawahinAPI.Core.Entities
{
    public class Chargers
    {
        [Key]
        public Guid Id { get; set; }

        public string? ChargerName { get; set; }

        [ForeignKey("ChargerType")]
        public Guid ChargerTypeId { get; set; }

        public ChargerPower? PowerKw { get; set; }
        public string? ElectricType { get; set; }
        public string? ChargerStatus { get; set; }
        public double? ChargerCost { get; set; }
        public string? ImageUrl { get; set; }
        public string? ParkingType { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }
        public PaymentType? PaymentType { get; set; }
        public CurrentChargerStatus? CurrentChargerStatus { get; set; }
        public ChargerType? ChargerType { get; set; }
    }

}
