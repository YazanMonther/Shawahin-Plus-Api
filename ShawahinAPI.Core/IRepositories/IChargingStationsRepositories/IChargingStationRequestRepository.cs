using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{

    /// <summary>
    /// Repository for managing charging station requests.
    /// </summary>
    public interface IChargingStationRequestRepository
    {
        /// <summary>
        /// Asynchronously adds a new charging station request.
        /// </summary>
        /// <param name="request">The charging station request to add.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto> AddRequestAsync(ChargingStationRequests request);

        /// <summary>
        /// Asynchronously retrieves all charging station requests.
        /// </summary>
        /// <returns>A collection of all charging station requests.</returns>
        Task<IEnumerable<ChargingStationRequests>> GetAllRequestsAsync();

        /// <summary>
        /// Asynchronously retrieves a charging station request by ID.
        /// </summary>
        /// <param name="requestId">The ID of the charging station request to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the charging station request with the specified ID.</returns>
        Task<ChargingStationRequests?> GetRequestByIdAsync(Guid requestId);

        /// <summary>
        /// Asynchronously updates the status of a charging station request.
        /// </summary>
        /// <param name="requestId">The ID of the charging station request to update.</param>
        /// <param name="status">The new status to set for the request.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> UpdateRequestStatusAsync(Guid? requestId, RequestStatus? status);

        /// <summary>
        /// Asynchronously removes a charging station request.
        /// </summary>
        /// <param name="request">The charging station request to remove.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> RemoveRequestAsync(ChargingStationRequests? request);
    }
}
