using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities
{
    public class CommunityPosts
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string? PostText { get; set; }
        public string? PostType { get; set; }
        public double Upvotes { get; set; }

        public ApplicationUser? User { get; set; }
        public ICollection<CommunityComments>? Comments { get; set; }
    }
}
