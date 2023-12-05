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
    public interface IStationGetRepository
    {

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

        Task<IEnumerable<ChargingStations?>?> GetChargingStationsByPower(ChargerPower? power);

        Task<IEnumerable<ChargingStations?>> GetChargerStationByChargerTypeAsync(ChargersType? type);

    }
}
