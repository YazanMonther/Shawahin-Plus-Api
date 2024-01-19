using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.CommunityDto
{
    /// <summary>
    /// Data Transfer Object for community comment.
    /// </summary>
    public class CommunityCommentBaseDto
    {

        public Guid UserId { get; set; }


        public Guid PostId { get; set; }

        public string? CommentText { get; set; }

    }
}
