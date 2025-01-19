using ShawahinAPI.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ShawahinAPI.Core.Entities.ChargingStationsEntities
{
    public class ChargerType : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ChargersType? Charger_Type { get; set; }

        public string? ChargerLogoUrl { get; set; }

        public IEnumerable<Chargers>? chargers { get; set; }
    }
}
