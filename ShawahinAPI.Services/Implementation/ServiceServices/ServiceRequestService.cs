using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.Entities.ServicesEntitiess;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract.IServiceServices;
using ShawahinAPI.Services.Implementation.Helpers;

namespace ShawahinAPI.Services.Implementation.ServiceServices
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IRepository<ServiceRequest> _repository;
        private readonly IRepository<ServiceInfo> _serviceInfoRepository;
        private readonly IRepository<ServiceType> _serviceTypeRepository;
        public ServiceRequestService(IRepository<ServiceRequest> repository ,
          IRepository<ServiceInfo> serviceInfoRepository,
          IRepository<ServiceType> serviceTypeRepository)
        {
            _repository = repository;
            _serviceInfoRepository = serviceInfoRepository;
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<IEnumerable<ServiceRequestResponseDto?>> GetAllServiceRequestsAsync()
        {
            // Retrieve all service requests
            var entities = await _repository.GetAllAsync();

            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<ServiceRequestResponseDto?>();
            }

            // Convert entities to response DTOs
            return await ServiceDataHelper.ToResponseRequestList(
                entities,
                _serviceInfoRepository,
                _serviceTypeRepository
            );
        }


        public async Task<ServiceRequestResponseDto> GetServiceRequestByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if(entity == null)
            {
                throw new ArgumentNullException("No Request with this service id .");
            }

            return await ServiceDataHelper.ToResponseRequest(entity, _serviceInfoRepository, _serviceTypeRepository);
        }

        public async Task<ResultDto> AddServiceRequestAsync(ServiceAddRequest requestDto )
        {

            var serviceReqBase = ToServiceRequestMapper.toServiceReqeustBase(requestDto);

            var serviceInfoBase = ToServiceRequestMapper.toServiceInfoBase(requestDto);

            var serviceInfo = ToServiceRequestMapper.toServiceInfo(serviceInfoBase);

            var serviceRequest = ToServiceRequestMapper.toServiceRequest(serviceReqBase, serviceInfo);

            serviceRequest.RequestStatus = "Pending";

            var addInfo = await _serviceInfoRepository.AddAsync(serviceInfo);
            var result = await _repository.AddAsync(serviceRequest);
            
            return result;
        }

        public async Task<ResultDto> RemoveServiceRequestAsync(Guid id)
        {

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                // Handle not found scenario
                return new ResultDto {Succeeded = false, Message = "Service request not found." };
            }

            var result = await _repository.RemoveAsync(entity);
            return result;
        }

    }

}
