using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShawahinAPI.Core.Entities
{
    public abstract class ChargingStationBase :BaseEntity
    {
        
        [ForeignKey("Contact")]
        public Guid ContactId { get; set; }

        [ForeignKey("Location")]
        public Guid LocationId { get; set; }

        [ForeignKey("StationOpeningHours")]
        public Guid StationOpeningHoursId { get; set; }

        [ForeignKey("Charges")]
        public Guid ChargesId { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }

        public ApplicationUser? User { get; set; }
        public Contacts? Contact { get; set; }
        public Locations? Location { get; set; }
        public StationOpeningHours? StationOpeningHours { get; set; }
        public Chargers? Chargers { get; set; }
    }
}