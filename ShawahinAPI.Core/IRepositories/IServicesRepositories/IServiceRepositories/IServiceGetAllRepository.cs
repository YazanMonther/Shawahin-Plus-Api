using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRepository
{
    /// <summary>
    /// Repository for managing Services.
    /// </summary>
    public interface IServiceGetAllRepository
    {
        /// <summary>
        /// Get all Service items.
        /// </summary>
        Task<IEnumerable<Services>> GetAllAsync();
    }
}
