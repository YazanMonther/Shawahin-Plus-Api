
namespace ShawahinAPI.Core.DTO.CommunityDto
{

        public class CommunityPostBaseDto
        {

            public Guid UserId { get; set; }

            public string? PostText { get; set; }

            public string? PostType { get; set; }

            public double Upvotes { get; set; }

        }
}
