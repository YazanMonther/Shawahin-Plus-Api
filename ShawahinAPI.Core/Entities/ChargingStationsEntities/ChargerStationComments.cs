using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities.ChargingStationsEntities
{
    public class ChargerStationComments
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("ChargingStation")]
        public Guid StationId { get; set; }

        public string? CommentText { get; set; }

        public ApplicationUser? User { get; set; }
        public ChargingStations? ChargingStation { get; set; }

    }
}
