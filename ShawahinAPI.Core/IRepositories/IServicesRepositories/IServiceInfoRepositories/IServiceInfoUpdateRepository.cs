using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServiceRepositories.IServiceInfoRepository
{
    /// <summary>
    /// Repository for updating an existing Service Information item.
    /// </summary>
    public interface IServiceInfoUpdateRepository
    {
        /// <summary>
        /// Update an existing Service Information item.
        /// </summary>
        Task UpdateAsync(ServiceInfo serviceInfo);
    }
}
