using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServiceRepositories.IServiceInfoRepository
{
    /// <summary>
    /// Repository for adding a new Service Information item.
    /// </summary>
    public interface IServiceInfoAddRepository
    {
        /// <summary>
        /// Add a new Service Information item.
        /// </summary>
        Task<ResultDto> AddAsync(ServiceInfo serviceInfo);
    }
}
