using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IChargingStationsServices
{
    /// <summary>
    /// Service for managing and retrieving charging stations.
    /// </summary>
    public interface IChargingStationsService
    {
        /// <summary>
        /// Add a new charging station.
        /// </summary>
        /// <param name="requestId">Charging station request id data to add.</param>
        /// <param name="userId">User Id for admin validation</param>
        /// <returns>A result indicating success or failure and an optional error message.</returns>
        Task<ResultDto?> AddNewChargingStationAsync(Guid? requestId, Guid? userId);

        /// <summary>
        /// Get a list of all charging stations.
        /// </summary>
        /// <returns>A list of charging station DTOs.</returns>
        Task<IEnumerable<ChargingStationDto?>> GetAllChargingStationsAsync();

        /// <summary>
        /// Get details of a specific charging station by ID.
        /// </summary>
        /// <param name="stationId">The ID of the charging station to retrieve.</param>
        /// <returns>The charging station DTO.</returns>
        Task<ChargingStationDto?> GetChargingStationByIdAsync(Guid? stationId);

        /// <summary>
        /// Update an existing charging station.
        /// </summary>
        /// <param name="stationId">The ID of the charging station to update.</param>
        /// <param name="updatedStation">Updated charging station data.</param>
        /// <returns>A result indicating success or failure and an optional error message.</returns>
        Task<ResultDto?> UpdateChargingStationAsync(Guid? stationId, ChargingStationDto updatedStation);

        /// <summary>
        /// Remove a charging station.
        /// </summary>
        /// <param name="stationId">The ID of the charging station to remove.</param>
        /// <returns>A result indicating success or failure and an optional error message.</returns>
        Task<ResultDto?> RemoveChargingStationAsync(Guid? stationId);

        /// <summary>
        /// Asynchronously retrieves charging stations based on multiple filtering parameters.
        /// </summary>
        /// <param name="chargerType">The type of charger to filter by.</param>
        /// <param name="paymentMethod">The payment method to filter by.</param>
        /// <param name="paymentType">The payment type to filter by.</param>
        /// <param name="chargerPower">The power of the charger to filter by.</param>
        /// <param name="chargerStatus">The status of the charger to filter by.</param>
        /// <returns>A collection of charging stations matching the specified filtering parameters.</returns>
        Task<IEnumerable<ChargingStationDto?>> GetFilteredChargingStations(
            string? chargerType,
            string? paymentMethod,
            string? paymentType,
            string? chargerPower,
            string? chargerStatus);

        /// <summary>
        /// Get charging stations added by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve stations for.</param>
        /// <returns>A list of charging station DTOs added by the specified user.</returns>
        Task<IEnumerable<UserStationsDto?>> GetChargingStationsByUserIdAsync(Guid? userId);

    }
}
