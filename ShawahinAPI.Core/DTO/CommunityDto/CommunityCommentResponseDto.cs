using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.CommunityDto
{
    public class CommunityCommentResponseDto : CommunityCommentBaseDto
    {
        public Guid Id { get; set; }
        public string? name { get; set; }

    }
}
