using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRequestRepositories
{
    /// <summary>
    /// Repository for adding a new Service Request.
    /// </summary>
    public interface IServiceRequestAddRepository
    {
        /// <summary>
        /// Add a new Service Request.
        /// </summary>
        Task<ResultDto> AddAsync(ServiceRequest serviceRequest);
    }
}
