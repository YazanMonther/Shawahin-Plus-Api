using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.CommunityDto
{
    public class CommunityEventBaseDto
    {

        public Guid UserPostId { get; set; }

        public string? EventName { get; set; }


        public string? EventDescription { get; set; }


        public DateTime EventDate { get; set; }


        public string? Location { get; set; }

        public string? ImageUrl { get; set; }
    }
}
