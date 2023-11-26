using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServiceRepositories.IServiceInfoRepository
{
    /// <summary>
    /// Repository for retrieving Service Information by ID.
    /// </summary>
    public interface IServiceInfoGetByIdRepository
    {
        /// <summary>
        /// Get Service Information by ID.
        /// </summary>
        Task<ServiceInfo> GetByIdAsync(Guid serviceInfoId);
    }
}
