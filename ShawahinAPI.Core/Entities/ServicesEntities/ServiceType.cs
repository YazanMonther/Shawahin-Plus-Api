using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities
{
    public class ServiceType
    {
        [Key]
        public Guid Id { get; set; }
        public string? ServiceTypeName { get; set; }

        public ICollection<ServiceInfo>? Services { get; set; }
    }
}
