using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRepository
{
    /// <summary>
    /// Repository for removing a Service item.
    /// </summary>
    public interface IServiceRemoveRepository
    {
        /// <summary>
        /// Remove a Service item.
        /// </summary>
        Task RemoveAsync(Services service);
    }
}
