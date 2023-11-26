using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRequestRepositories
{
    /// <summary>
    /// Repository for removing a Service Request.
    /// </summary>
    public interface IServiceRequestRemoveRepository
    {
        /// <summary>
        /// Remove a Service Request.
        /// </summary>
        Task RemoveAsync(ServiceRequest serviceRequest);
    }
}
