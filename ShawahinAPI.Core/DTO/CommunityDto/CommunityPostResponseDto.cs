using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.CommunityDto
{
    public  class CommunityPostResponseDto : CommunityPostBaseDto
    {
        public Guid PostId { get; set; }
    }
}
