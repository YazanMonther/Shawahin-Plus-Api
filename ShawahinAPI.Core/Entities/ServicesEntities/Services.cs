using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShawahinAPI.Core.Entities.ServicesEntities
{

    public class Services
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ServiceInfo")]
        public Guid ServiceInfoId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public ApplicationUser? User { get; set; }
        public ServiceInfo? ServiceInfo { get; set; }

    }
}
