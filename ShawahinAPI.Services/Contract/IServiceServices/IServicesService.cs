using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;

namespace ShawahinAPI.Services.Contract.IServiceServices
{
    /// <summary>
    /// Service contract for managing Services.
    /// </summary>
    public interface IServicesService
    {
        /// <summary>
        /// Gets all services asynchronously.
        /// </summary>
        /// <returns>A collection of services response DTOs.</returns>
        Task<IEnumerable<ServiceResponseDto?>> GetAllServicesAsync();

        /// <summary>
        /// Gets services by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the services.</param>
        /// <returns>The services response DTO.</returns>
        Task<ServiceResponseDto?> GetServicesByIdAsync(Guid id);

        /// <summary>
        /// Adds services asynchronously based on a service request.
        /// </summary>
        /// <param name="requestId">The ID of the service request.</param>
        /// <param name="userId">The ID of the user making the request.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> AddServicesAsync(Guid requestId, Guid userId);

        /// <summary>
        /// Updates existing services asynchronously.
        /// </summary>
        /// <param name="servicesDto">The services response DTO.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> UpdateServicesAsync(ServiceResponseDto servicesDto);

        /// <summary>
        /// Removes services by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the services to remove.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> RemoveServicesAsync(Guid id);
    }
}
