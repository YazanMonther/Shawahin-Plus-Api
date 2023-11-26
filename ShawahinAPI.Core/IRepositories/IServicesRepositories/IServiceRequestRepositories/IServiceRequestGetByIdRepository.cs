using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRequestRepositories
{
    /// <summary>
    /// Repository for retrieving a Service Request by ID.
    /// </summary>
    public interface IServiceRequestGetByIdRepository
    {
        /// <summary>
        /// Get a Service Request by ID.
        /// </summary>
        Task<ServiceRequest> GetByIdAsync(Guid serviceRequestId);
    }
}
