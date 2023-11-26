using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRequestRepositories
{
    /// <summary>
    /// Repository for getting accepted Service Requests.
    /// </summary>
    public interface IServiceRequestGetAcceptedRepository
    {
        /// <summary>
        /// Get accepted Service Requests.
        /// </summary>
        Task<IEnumerable<ServiceRequest>> GetAcceptedRequestsAsync();
    }
}
