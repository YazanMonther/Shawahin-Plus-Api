using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities
{
    public class CommunityEvents
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserPostId { get; set; }

        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string? Location { get; set; }
        public string? ImageUrl { get; set; }

        public ApplicationUser? User { get; set; }


    }

}
