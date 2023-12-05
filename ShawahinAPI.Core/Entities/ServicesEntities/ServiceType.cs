using ShawahinAPI.Core.Entities.ServicesEntities;
using System.ComponentModel.DataAnnotations;

namespace ShawahinAPI.Core.Entities.ServicesEntitiess
{
    public class ServiceType
    {
        [Key]
        public Guid Id { get; set; }
        public string? ServiceTypeName { get; set; }

        public ICollection<ServiceInfo>? Services { get; set; }
    }
}
