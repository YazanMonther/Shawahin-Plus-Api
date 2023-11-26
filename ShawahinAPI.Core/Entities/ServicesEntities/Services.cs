using ShawahinAPI.Core.Entities.ServicesEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShawahinAPI.Core.Entities
{

    public class Services
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ServiceInfo")]
        public Guid ServiceInfoId { get; set; }

        public ServiceInfo? ServiceInfo { get; set; }

    }
}
