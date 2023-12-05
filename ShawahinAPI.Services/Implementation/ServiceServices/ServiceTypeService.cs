using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ServicesEntitiess;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract.IServiceServices;

namespace ShawahinAPI.Services.Implementation.ServiceServices
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IRepository<ServiceType> _repository;

        public ServiceTypeService(IRepository<ServiceType> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceTypeResponseDto>> GetAllServiceTypesAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var serviceTypeDtos = EntityDtoMapper<ServiceType, ServiceTypeResponseDto>.MapToDto(entities);
                return serviceTypeDtos;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new ArgumentException($"can Reterive the data {ex}"); // Rethrow the exception for now
            }
        }

        public async Task<ServiceTypeResponseDto> GetServiceTypeByIdAsync(Guid id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                var serviceTypeDto = EntityDtoMapper<ServiceType, ServiceTypeResponseDto>.MapToDto(entity);
                return serviceTypeDto;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new ArgumentException($"can Reterive the data {ex}"); // Rethrow the exception for now
            }
        }

        public async Task<ResultDto> AddServiceTypeAsync(ServiceTypeBaseDto serviceTypeDto)
        {
            try
            {
                // Validate serviceTypeDto here if needed

                var entity = EntityDtoMapper<ServiceType, ServiceTypeBaseDto>.MapToEntity(serviceTypeDto);

                var result = await _repository.AddAsync(entity);

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new ArgumentException($"can Reterive the data {ex}"); // Rethrow the exception for now
            }
        }

        public async Task<ResultDto> UpdateServiceTypeAsync(ServiceTypeBaseDto serviceTypeDto , Guid TypeId)
        {
            try
            {
                var existingEntity = await _repository.GetByIdAsync(TypeId);

                if (existingEntity == null)
                {
                    // Handle not found scenario
                    return new ResultDto { Succeeded = false, Message = "Service type not found." };
                }

                existingEntity.ServiceTypeName = serviceTypeDto.ServiceTypeName;
                // Update existingEntity properties based on serviceTypeDto

                var result = await _repository.UpdateAsync(existingEntity);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new ArgumentException($"can Reterive the data {ex}"); // Rethrow the exception for now
            }
        }

        public async Task<ResultDto> RemoveServiceTypeAsync(Guid id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    // Handle not found scenario
                    return new ResultDto { Succeeded = false, Message = "Service type not found." };
                }

                var result = await _repository.RemoveAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new ArgumentException($"can Reterive the data {ex}"); // Rethrow the exception for now
            }
        }

    }
}
