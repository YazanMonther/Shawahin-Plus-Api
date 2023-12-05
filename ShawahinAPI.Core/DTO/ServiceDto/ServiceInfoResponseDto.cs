using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.DTO.ServiceDto
{
    public class ServiceInfoResponseDto : ServiceInfoBaseDto
    {
        public Guid Id { get; set; }
        public string? ServiceTypeName { get; set; }

    }
}
