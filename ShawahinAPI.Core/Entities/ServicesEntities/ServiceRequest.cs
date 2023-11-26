using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Core.Entities.ServicesEntities
{
    public class ServiceRequest
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("ServiceInfo")]
        public Guid ServiceInfoId { get; set; }

        public string? RequestStatus { get; set; }

        public ApplicationUser? User { get; set; }
        public ServiceInfo? ServiceInfo { get; set; }
    }


}
