using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IServiceServices
{
    /// <summary>
    /// Service contract for managing Service Requests.
    /// </summary>
    public interface IServiceRequestService
    {
        /// <summary>
        /// Gets all service requests asynchronously.
        /// </summary>
        /// <returns>A collection of service request response DTOs.</returns>
        Task<IEnumerable<ServiceRequestResponseDto?>> GetAllServiceRequestsAsync();

        /// <summary>
        /// Gets a service request by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the service request.</param>
        /// <returns>The service request response DTO.</returns>
        Task<ServiceRequestResponseDto> GetServiceRequestByIdAsync(Guid id);

        /// <summary>
        /// Adds a new service request asynchronously.
        /// </summary>
        /// <param name="requestDto">The service request request DTO.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> AddServiceRequestAsync(ServiceAddRequest requestDto);


        /// <summary>
        /// Removes a service request by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the service request to remove.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> RemoveServiceRequestAsync(Guid id);


        Task<ResultDto> DenayServiceRequestAsync(Guid? requestId);
    }

}
