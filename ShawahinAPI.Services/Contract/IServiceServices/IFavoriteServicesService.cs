using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ServicesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IServiceServices
{
    public interface IFavoriteServicesService
    {
        Task<IEnumerable<ServiceResponseDto>> GetFavoriteServicesAsync(Guid userId);

        Task<bool> IsServiceInFavoritesAsync(Guid userId, Guid serviceId);

        Task<ResultDto> AddServiceToFavoritesAsync(Guid userId, Guid serviceId);

        Task<ResultDto> RemoveServiceFromFavoritesAsync(Guid userId, Guid serviceId);
    }

}
