using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;

namespace ShawahinAPI.Services.Contract.IServiceServices
{
    /// <summary>
    /// Service contract for managing Service Types.
    /// </summary>
    public interface IServiceTypeService
    {
        /// <summary>
        /// Gets all service types asynchronously.
        /// </summary>
        /// <returns>A collection of service type response DTOs.</returns>
        Task<IEnumerable<ServiceTypeResponseDto>> GetAllServiceTypesAsync();

        /// <summary>
        /// Gets a service type by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the service type.</param>
        /// <returns>The service type response DTO.</returns>
        Task<ServiceTypeResponseDto> GetServiceTypeByIdAsync(Guid id);

        /// <summary>
        /// Adds a new service type asynchronously.
        /// </summary>
        /// <param name="serviceTypeDto">The service type base DTO.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> AddServiceTypeAsync(ServiceTypeBaseDto serviceTypeDto);

        /// <summary>
        /// Updates an existing service type asynchronously.
        /// </summary>
        /// <param name="serviceTypeDto">The service type response DTO.</param>
        /// <param name="TypeId"> Service Type Id</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> UpdateServiceTypeAsync(ServiceTypeBaseDto serviceTypeDto ,  Guid TypeId);

        /// <summary>
        /// Removes a service type by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the service type to remove.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> RemoveServiceTypeAsync(Guid id);
    }
}
