using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Services.Contract.IServiceServices;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Services.Implementation.Helpers;
using ShawahinAPI.Core.Entities.ServicesEntitiess;

namespace ShawahinAPI.Services.Implementation.ServiceServices
{
    public class ServicesService : IServicesService
    {
        private readonly IRepository<ShawahinAPI.Core.Entities.ServicesEntities.Services> _repository;
        private readonly IRepository<ServiceRequest> _serviceRequestRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<ServiceInfo> _serviceInfoRepository;
        private readonly IRepository<ServiceType> _serviceTypeRepository;
        public ServicesService(
            IRepository<ShawahinAPI.Core.Entities.ServicesEntities.Services> repository,
            IRepository<ServiceRequest> serviceRequestRepository,
            IRepository<ApplicationUser> userRepository , IRepository<ServiceInfo> repositoryInfo,
            IRepository<ServiceType> repositoryType)
        {
            _repository = repository;
            _serviceRequestRepository = serviceRequestRepository;
            _userRepository = userRepository;
            _serviceInfoRepository = repositoryInfo;
            _serviceTypeRepository = repositoryType;
        }

        public async Task<IEnumerable<ServiceResponseDto?>> GetAllServicesAsync()
        {
            var entities = await _repository.GetAllAsync();

            if(entities == null)
            {
              throw new ArgumentNullException("No Request  services .");

            }
            return await ServiceDataHelper.ToResponseList(
                entities,
                _serviceInfoRepository,
                _serviceTypeRepository
            );
        }

        public async Task<ServiceResponseDto?> GetServicesByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                // Handle the scenario where the service with the given ID is not found
                // You might want to return null or throw an exception based on your requirements
                return null;
            }

            return await ServiceDataHelper.ToResponse(
                entity,
                _serviceInfoRepository,
                _serviceTypeRepository
            );
        }

        public async Task<ResultDto> AddServicesAsync(Guid requestId, Guid userId)
        {
            // Check if the user is an admin
            var user = await _userRepository.GetByIdAsync(userId);

            var isAdminHelper = await UserAuthHelper.ValidateUserAdminId(userId, _userRepository);

            if (!isAdminHelper.Succeeded)
                return isAdminHelper;

            // Check if the request exists
            var request = await _serviceRequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                return new ResultDto { Succeeded = false, Message = "Service request not found." };
            }

            // Check if the request status allows adding services
            if (request.RequestStatus != "Pending")
            {
                return new ResultDto { Succeeded = false, Message = "Cannot add services to a request with status other than 'Pending'." };
            }

            // Update request status
            request.RequestStatus = "Processed";
            await _serviceRequestRepository.UpdateAsync(request);

            // Add the service
            var service = new ShawahinAPI.Core.Entities.ServicesEntities.Services
            {
                ServiceInfoId = request.ServiceInfoId,
                UserId = request.UserId,
                ServiceInfo = request.ServiceInfo,
                Id = Guid.NewGuid(),
                User =request.User
            };

            var result = await _repository.AddAsync(service);
            return result;
        }

        public async Task<ResultDto> UpdateServicesAsync(ServiceResponseDto servicesDto)
        {
            var existingEntity = await _repository.GetByIdAsync(servicesDto.Id);

            if (existingEntity == null)
            {
                // Handle not found scenario
                return new ResultDto { Succeeded = false, Message = "Service not found." };
            }

            // Update existingEntity properties based on servicesDto

            var result = await _repository.UpdateAsync(existingEntity);
            return result;
        }

        public async Task<ResultDto> RemoveServicesAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                // Handle not found scenario
                return new ResultDto { Succeeded = false, Message = "Service not found." };
            }

            var result = await _repository.RemoveAsync(entity);
            return result;
        }
    }
}
