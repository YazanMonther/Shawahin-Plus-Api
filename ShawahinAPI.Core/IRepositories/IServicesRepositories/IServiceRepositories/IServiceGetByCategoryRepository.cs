using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRepository
{
    /// <summary>
    /// Repository for retrieving Services by category.
    /// </summary>
    public interface IServiceGetByCategoryRepository
    {
        /// <summary>
        /// Get Services by category.
        /// </summary>
        Task<IEnumerable<Services>> GetByCategoryAsync(ServiceType category);
    }
}
