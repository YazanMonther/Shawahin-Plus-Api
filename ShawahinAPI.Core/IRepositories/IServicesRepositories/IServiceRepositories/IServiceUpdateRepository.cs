using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRepository
{
    /// <summary>
    /// Repository for updating an existing Service item.
    /// </summary>
    public interface IServiceUpdateRepository
    {
        /// <summary>
        /// Update an existing Service item.
        /// </summary>
        Task UpdateAsync(Services service);
    }
}
