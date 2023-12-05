using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Services.Contract.IServiceServices;
using ShawahinAPI.Core.Entities.ServicesEntitiess;

namespace ShawahinAPI.Services.Implementation.ServiceServices
{
    public class FavoriteService : IFavoriteServicesService
    {

        private readonly IRepository<FavoriteServices> _favoriteServicesRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<ServiceInfo> _serviceInfo;
        private readonly IRepository<ServiceType> _serviceType;
        IRepository<ShawahinAPI.Core.Entities.ServicesEntities.Services> _servicesRepository;

        public FavoriteService(
            IRepository<FavoriteServices> favoriteServicesRepository,
            IRepository<ApplicationUser> userRepository,
            IRepository<ShawahinAPI.Core.Entities.ServicesEntities.Services> servicesRepository,
            IRepository<ServiceInfo> info,
            IRepository<ServiceType> type)
        {
            _favoriteServicesRepository = favoriteServicesRepository ?? throw new ArgumentNullException(nameof(favoriteServicesRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _servicesRepository = servicesRepository ?? throw new ArgumentNullException(nameof(servicesRepository));
            _serviceInfo = info;
            _serviceType = type;
        }

        public async Task<IEnumerable<ServiceResponseDto>> GetFavoriteServicesAsync(Guid userId)
        {
            var favoriteServices = await _favoriteServicesRepository
                .GetByConditionAsync(fs => fs.UserId == userId);

            var favoriteServiceDtos = await Task.WhenAll(favoriteServices.Select(async fs =>
            {
                var service = await _servicesRepository.GetByIdAsync(fs.ServiceId);
                var serviceInfo = await _serviceInfo.GetByIdAsync(fs.Service.ServiceInfoId);
                var serviceType = await _serviceType.GetByIdAsync(serviceInfo.ServiceTypeId);
                return new ServiceResponseDto
                {
                    Id = fs.Id,
                    ServiceTypeName = serviceType.ServiceTypeName,
                    ServiceName = serviceInfo.ServiceName,
                    Description = serviceInfo.Description,
                    PhoneNumber = serviceInfo.PhoneNumber,
                    City = serviceInfo.City,
                    Address = serviceInfo.Address,
                    ImageUrl = serviceInfo.ImageUrl,
                    ServiceTypeId = serviceInfo.ServiceTypeId
                };
            }));

            return favoriteServiceDtos;
        }
        
        public async Task<bool> IsServiceInFavoritesAsync(Guid userId, Guid serviceId)
        {
            var fav = await _favoriteServicesRepository
                .GetByConditionAsync(fs => fs.UserId == userId && fs.ServiceId == serviceId);

            return fav.Any();
        }

        public async Task<ResultDto> AddServiceToFavoritesAsync(Guid userId, Guid serviceId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var service = await _servicesRepository.GetByIdAsync(serviceId);

            if (user == null || service == null)
            {
                return new ResultDto { Succeeded = false, Message = "User or service not found." };
            }

            var isServiceInFavorites = await IsServiceInFavoritesAsync(userId, serviceId);

            if (isServiceInFavorites)
            {
                return new ResultDto { Succeeded = false, Message = "Service is already in favorites." };
            }

            var favoriteService = new FavoriteServices
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ServiceId = serviceId
            };

            return await _favoriteServicesRepository.AddAsync(favoriteService);
           
        }

        public async Task<ResultDto> RemoveServiceFromFavoritesAsync(Guid userId, Guid serviceId)
        {
            var favoriteServices = await _favoriteServicesRepository
                .GetByConditionAsync(fs => fs.UserId == userId && fs.ServiceId == serviceId);

            if (favoriteServices == null)
            {
                return new ResultDto { Succeeded = false, Message = "Favorite service not found." };
            }
            var fav = favoriteServices.FirstOrDefault();

            if (fav == null)
            {
                return new ResultDto { Succeeded = false, Message = "Favorite service not found." };
            }

            return await _favoriteServicesRepository.RemoveAsync(fav); ;
        }
    }
}
