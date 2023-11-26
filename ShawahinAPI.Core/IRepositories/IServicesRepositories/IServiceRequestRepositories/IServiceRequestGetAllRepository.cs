using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRequestRepositories
{
    /// <summary>
    /// Repository for getting all Service Requests.
    /// </summary>
    public interface IServiceRequestGetAllRepository
    {
        /// <summary>
        /// Get all Service Request items.
        /// </summary>
        Task<IEnumerable<ServiceRequest>> GetAllAsync();
    }
}
