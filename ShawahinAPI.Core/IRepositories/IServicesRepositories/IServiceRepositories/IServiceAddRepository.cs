using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRepository
{
    /// <summary>
    /// Repository for adding a new Service item.
    /// </summary>
    public interface IServiceAddRepository
    {
        /// <summary>
        /// Add a new Service item.
        /// </summary>
        Task<ResultDto> AddAsync(Services service);
    }
}
