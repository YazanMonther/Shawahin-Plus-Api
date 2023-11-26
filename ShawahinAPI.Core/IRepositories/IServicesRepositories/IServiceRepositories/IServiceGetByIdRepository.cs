using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRepository
{
    /// <summary>
    /// Repository for retrieving Services by ID.
    /// </summary>
    public interface IServiceGetByIdRepository
    {
        /// <summary>
        /// Get Service by ID.
        /// </summary>
        Task<Services> GetByIdAsync(Guid serviceId);
    }
}
