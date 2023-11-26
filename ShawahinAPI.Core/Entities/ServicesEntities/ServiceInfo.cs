using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.Entities.ServicesEntities
{
    public class ServiceInfo
    {
        [Key]
        public Guid Id { get; set; }

        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public string?  ContactInformation { get; set; }

        [ForeignKey("ServiceType")]
        public Guid ServiceTypeId { get; set; }

        public ServiceType? ServiceType { get; set; }
    }
}
