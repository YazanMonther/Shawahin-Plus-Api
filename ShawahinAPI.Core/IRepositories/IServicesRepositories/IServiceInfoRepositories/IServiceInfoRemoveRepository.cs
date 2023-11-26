using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServiceRepositories.IServiceInfoRepository
{

    /// <summary>
    /// Repository for removing a Service Information item.
    /// </summary>
    public interface IServiceInfoRemoveRepository
    {
        /// <summary>
        /// Remove a Service Information item.
        /// </summary>
        Task RemoveAsync(ServiceInfo serviceInfo);
    }
}
