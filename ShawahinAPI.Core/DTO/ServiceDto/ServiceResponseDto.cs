using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ServiceDto
{
    public class ServiceResponseDto : ServiceRequestBaseDto
    {
        public Guid Id { get; set; }
        public string? ServiceTypeName { get; set; }
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

    }
}
