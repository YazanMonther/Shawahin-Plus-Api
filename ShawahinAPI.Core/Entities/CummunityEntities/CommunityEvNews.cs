using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities.CummunityEntities
{
    public class CommunityEvNews
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserPostId { get; set; }

        public string? Title { get; set; }
        public string? Content { get; set; }

        public string? ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }

        public ApplicationUser? User { get; set; }
    }

}
