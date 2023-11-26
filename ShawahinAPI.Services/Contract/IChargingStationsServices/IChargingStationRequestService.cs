using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Services.Contract.IChargingStationsServices
{
    /// <summary>
    /// Service for managing charging station requests.
    /// </summary>
    public interface IChargingStationRequestService
    {
        /// <summary>
        /// Add a new charging station request.
        /// </summary>
        /// <param name="stationDto">Charging station request data to add.</param>
        /// <returns>A result indicating success or failure and an optional error message.</returns>
        Task<ResultDto?> AddChargingStationRequestAsync(AddChargingStationsReqDto? stationDto);

        /// <summary>
        /// Get all charging station requests for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve requests for.</param>
        /// <returns>A collection of charging station request DTOs.</returns>
        Task<IEnumerable<ChargingStationsReqResponseDto>?> GetAllChargingStationRequestsAsync(Guid? userId);

        /// <summary>
        /// Get a charging station request by ID.
        /// </summary>
        /// <param name="requestId">The ID of the charging station request to retrieve.</param>
        /// <returns>The charging station request DTO.</returns>
        Task<ChargingStationsReqResponseDto?> GetChargingStationRequestByIdAsync(Guid? requestId);

        /// <summary>
        /// Update the status of a charging station request.
        /// </summary>
        /// <param name="requestId">The ID of the charging station request to update.</param>
        /// <param name="status">The new status to set for the request.</param>
        /// <returns>A result indicating success or failure and an optional error message.</returns>
        Task<ResultDto?> UpdateChargingStationRequestStatusAsync(Guid? requestId, RequestStatus? status);

        /// <summary>
        /// Remove a charging station request.
        /// </summary>
        /// <param name="requestId">The ID of the charging station request to remove.</param>
        /// <returns>A result indicating success or failure and an optional error message.</returns>
        Task<ResultDto?> RemoveChargingStationRequestAsync(Guid? requestId);
    }
}
