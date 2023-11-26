using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{

    /// <summary>
    /// Repository for managing charging stations.
    /// </summary>
    public interface IChargingStationRepository
    {
        /// <summary>
        /// Asynchronously adds a new charging station.
        /// </summary>
        /// <param name="station">The charging station to add.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> AddStationAsync(ChargingStations? station);

        /// <summary>
        /// Asynchronously retrieves charging stations added by a user (for admin users).
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve stations for.</param>
        /// <returns>A collection of charging stations added by the specified user.</returns>
        Task<IEnumerable<ChargingStations?>> GetStationsAddedByUserAsync(Guid? userId);

        /// <summary>
        /// Asynchronously retrieves all charging stations.
        /// </summary>
        /// <returns>A collection of all charging stations.</returns>
        Task<IEnumerable<ChargingStations?>> GetAllStationsAsync();

        /// <summary>
        /// Asynchronously retrieves a charging station by ID.
        /// </summary>
        /// <param name="stationId">The ID of the charging station to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the charging station with the specified ID.</returns>
        Task<ChargingStations?> GetStationByIdAsync(Guid? stationId);

        /// <summary>
        /// Asynchronously removes a charging station.
        /// </summary>
        /// <param name="station">The charging station to remove.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto> RemoveStationAsync(ChargingStations? station);

        /// <summary>
        /// Asynchronously updates an existing charging station.
        /// </summary>
        /// <param name="station">The charging station to update.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> UpdateStationAsync(ChargingStations? station);

        /// <summary>
        /// Asynchronously retrieves charging stations by payment method.
        /// </summary>
        /// <param name="paymentMethod">The payment method to filter by.</param>
        /// <returns>A collection of charging stations matching the specified payment method.</returns>
        Task<IEnumerable<ChargingStations?>> GetStationsByPaymentMethodAsync(PaymentMethod? paymentMethod);

        /// <summary>
        /// Asynchronously retrieves charging stations by payment type.
        /// </summary>
        /// <param name="paymentType">The payment type to filter by.</param>
        /// <returns>A collection of charging stations matching the specified payment type.</returns>
        Task<IEnumerable<ChargingStations?>> GetStationsByPaymentTypeAsync(PaymentType? paymentType);

        /// <summary>
        /// Asynchronously retrieves charging stations by charger status.
        /// </summary>
        /// <param name="CurrentChargerStatus">The charger status to filter by.</param>
        /// <returns>A collection of charging stations matching the specified charger status.</returns>
        Task<IEnumerable<ChargingStations?>> GetStationsByChargerStatusAsync(CurrentChargerStatus? chargerStatus);

        /// <summary>
        /// Asynchronously retrieves charging stations added by a user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve stations for.</param>
        /// <returns>A collection of charging stations added by the specified user.</returns>
        Task<IEnumerable<ChargingStations?>> GetStationsByUserIdAsync(Guid? userId);

    }
}
