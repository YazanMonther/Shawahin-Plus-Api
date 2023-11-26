using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServiceRepositories.IServiceInfoRepository
{
    /// <summary>
    /// Repository for managing Service Information.
    /// </summary>
    public interface IServiceInfoGetAllRepository
    {
        /// <summary>
        /// Get all Service Information items.
        /// </summary>
        Task<IEnumerable<ServiceInfo>> GetAllAsync();
    }
}
