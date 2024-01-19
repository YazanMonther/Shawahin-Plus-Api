using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Services.Contract.IServiceServices;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Services.Implementation.Helpers;
using ShawahinAPI.Core.Entities.ServicesEntitiess;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Services.Contract;
using ShawahinAPI.Core.DTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using static System.Collections.Specialized.BitVector32;

namespace ShawahinAPI.Services.Implementation.ServiceServices
{
    public class ServicesService : IServicesService
    {
        private readonly IRepository<ShawahinAPI.Core.Entities.ServicesEntities.Services> _repository;
        private readonly IRepository<ServiceRequest> _serviceRequestRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<ServiceInfo> _serviceInfoRepository;
        private readonly IRepository<Core.Entities.ServicesEntitiess.ServiceType> _serviceTypeRepository;
        private readonly IEmailService _emailService;

        public ServicesService(
            IRepository<ShawahinAPI.Core.Entities.ServicesEntities.Services> repository,
            IRepository<ServiceRequest> serviceRequestRepository,
            IRepository<ApplicationUser> userRepository , IRepository<ServiceInfo> repositoryInfo,
            IRepository<Core.Entities.ServicesEntitiess.ServiceType> repositoryType  
            , IEmailService email
            )
        {
            _repository = repository;
            _serviceRequestRepository = serviceRequestRepository;
            _userRepository = userRepository;
            _serviceInfoRepository = repositoryInfo;
            _serviceTypeRepository = repositoryType;
            _emailService = email;
           
        }

        public async Task<IEnumerable<ServiceResponseDto?>> GetAllServicesAsync()
        {
            var entities = await _repository.GetAllAsync(entity => entity.ServiceInfo,
                entity => entity.ServiceInfo.ServiceType);

            if(entities == null)
            {
              throw new ArgumentNullException("No Request  services .");
            }
            return await ServiceDataHelper.ToResponseList(
                entities
            );
        }

        public async Task<IEnumerable<ServiceResponseDto?>> GetServicesByTypeIdAsync(Guid typeId)
        {
            var entities = await _repository.GetByConditionAsync(se =>se.ServiceInfo.ServiceTypeId ==typeId, entity => entity.ServiceInfo,
                entity => entity.ServiceInfo.ServiceType);

            if (entities == null)
            {
                throw new ArgumentNullException("No Request  services .");
            }
            return await ServiceDataHelper.ToResponseList(
                entities
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
            if (request.RequestStatus != RequestStatus.Pending.ToString())
            {
                return new ResultDto { Succeeded = false, Message = "Cannot add services to a request with status other than 'Pending'." };
            }

            // Update request status
            request.RequestStatus = RequestStatus.Accepted.ToString();
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

            if (result.Succeeded)
            {
                var User = await _userRepository.GetByIdAsync(userId);
                try
                {
                    EmailRequest emailRequestApproval = new EmailRequest()
                    {
                        ToEmail = User?.Email!,
                        Subject = "Service Request Approved",
                        Body = $"Dear User,\n\n" +
                                $"Congratulations! Your Service Request has been approved.\n" +
                                $"We look forward to providing you with excellent service.\n\n" +
                                $"Best regards,\n" +
                                $"Shawahin Plus"
                    };
                    await _emailService.SendEmailAsync(emailRequestApproval);

                }
                catch (Exception ex)
                {

                    return new ResultDto { Succeeded = result.Succeeded, Message = $"{result.Message}, Error sending email: {ex.Message}" };
                }
            }
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
