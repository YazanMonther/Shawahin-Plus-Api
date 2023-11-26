using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities
{
    public class CommunityComments
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("CommunityPost")]
        public Guid PostId { get; set; }

        public string? CommentText { get; set; }

        public ApplicationUser? User { get; set; }
        public CommunityPosts? CommunityPost { get; set; }
    }

}
