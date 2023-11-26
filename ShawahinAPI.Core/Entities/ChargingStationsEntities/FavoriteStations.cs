using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ShawahinAPI.Core.Entities.ChargingStationsEntities
{
    public class FavoriteStations
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Station")]
        public Guid StationId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public ChargingStations? Station { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
