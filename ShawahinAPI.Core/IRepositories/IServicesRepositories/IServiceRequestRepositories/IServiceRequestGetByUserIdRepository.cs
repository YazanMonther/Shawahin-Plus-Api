using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRequestRepositories
{
    /// <summary>
    /// Repository for getting Service Requests by user ID.
    /// </summary>
    public interface IServiceRequestGetByUserIdRepository
    {
        /// <summary>
        /// Get Service Requests by user ID.
        /// </summary>
        Task<IEnumerable<ServiceRequest>> GetByUserIdAsync(Guid userId);
    }
}
