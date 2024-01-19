using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.CommunityDto
{
    public class CommunityEvNewsBaseDto
    {
        public Guid UserPostId { get; set; }

        public string? Title { get; set; }
        
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
