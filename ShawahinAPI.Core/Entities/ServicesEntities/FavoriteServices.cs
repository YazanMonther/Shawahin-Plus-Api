using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities
{

    public class FavoriteServices
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Service")]
        public Guid ServiceId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public Services? Service { get; set; }
        public ApplicationUser? User { get; set; }
    }

}
