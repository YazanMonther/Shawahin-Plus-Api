using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract.IServiceServices;


namespace ShawahinAPI.Services.Implementation.ServiceServices
{
    public class ServiceInfoService : IServiceInfoService
    {
        private readonly IRepository<ServiceInfo> _repository;
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceInfoService(IRepository<ServiceInfo> repository, IServiceTypeService serviceTypeService)
        {
            _repository = repository;
            _serviceTypeService = serviceTypeService;
        }

        public async Task<IEnumerable<ServiceInfoResponseDto>> GetAllServiceInfosAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var serviceInfoDtos = EntityDtoMapper<ServiceInfo, ServiceInfoResponseDto>.MapToDto(entities);

                foreach (var serviceInfoDto in serviceInfoDtos)
                {
                    var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync(serviceInfoDto.ServiceTypeId);
                    serviceInfoDto.ServiceTypeName = serviceType?.ServiceTypeName;
                }

                return serviceInfoDtos;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw;
            }
        }

        public async Task<ServiceInfoResponseDto?> GetServiceInfoByIdAsync(Guid id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                var serviceInfoDto = EntityDtoMapper<ServiceInfo, ServiceInfoResponseDto>.MapToDto(entity);

                if (serviceInfoDto != null)
                {
                    var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync(serviceInfoDto.ServiceTypeId);
                    serviceInfoDto.ServiceTypeName = serviceType?.ServiceTypeName;
                }

                return serviceInfoDto;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw;
            }
        }

        public async Task<ResultDto> AddServiceInfoAsync(ServiceInfoBaseDto serviceInfoDto)
        {
            try
            {
                // Validate serviceInfoDto here if needed

                var entity = EntityDtoMapper<ServiceInfo, ServiceInfoBaseDto>.MapToEntity(serviceInfoDto);

                var result = await _repository.AddAsync(entity);

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw;
            }
        }

        public async Task<ResultDto> UpdateServiceInfoAsync(ServiceInfoResponseDto serviceInfoDto)
        {
            try
            {
                var existingEntity = await _repository.GetByIdAsync(serviceInfoDto.Id);

                if (existingEntity == null)
                {
                    // Handle not found scenario
                    return new ResultDto { Succeeded = false, Message = "Service info not found." };
                }

                // Update existingEntity properties based on serviceInfoDto

                var result = await _repository.UpdateAsync(existingEntity);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw;
            }
        }

        public async Task<ResultDto> RemoveServiceInfoAsync(Guid id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    // Handle not found scenario
                    return new ResultDto { Succeeded = false, Message = "Service info not found." };
                }

                var result = await _repository.RemoveAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw;
            }
        }
    }
}
