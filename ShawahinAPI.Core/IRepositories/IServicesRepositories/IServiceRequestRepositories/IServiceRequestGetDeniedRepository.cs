using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRequestRepositories
{
    /// <summary>
    /// Repository for getting denied Service Requests.
    /// </summary>
    public interface IServiceRequestGetDeniedRepository
    {
        /// <summary>
        /// Get denied Service Requests.
        /// </summary>
        Task<IEnumerable<ServiceRequest>> GetDeniedRequestsAsync();
    }
}
