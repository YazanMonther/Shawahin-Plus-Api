using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{
    /// <summary>
    /// Repository interface for managing charger types.
    /// </summary>
    public interface IChargerTypeRepository
    {
        /// <summary>
        /// Asynchronously adds a new charger type.
        /// </summary>
        /// <param name="chargerType">The charger type to add.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> AddChargerTypeAsync(ChargerType? chargerType);

        /// <summary>
        /// Asynchronously retrieves all charger types.
        /// </summary>
        /// <returns>A collection of all charger types.</returns>
        Task<IEnumerable<ChargerType?>> GetAllChargerTypesAsync();

        /// <summary>
        ///  retrieves  chargers by  types.
        /// </summary>
        /// <returns>A collection of  charger stations from same type.</returns>
        Task<IEnumerable<ChargingStations?>> GetChargerStationByChargerTypeAsync(ChargersType? type);
        /// <summary>
        /// Asynchronously retrieves a charger type by its ID.
        /// </summary>
        /// <param name="chargerTypeId">The ID of the charger type.</param>
        /// <returns>A task representing the asynchronous operation that returns the charger type with the specified ID.</returns>
        Task<ChargerType?> GetChargerTypeByIdAsync(Guid? chargerTypeId);

        /// <summary>
        /// Asynchronously removes a charger type (for admin users).
        /// </summary>
        /// <param name="chargerType">The charger type to remove.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> RemoveChargerTypeAsync(ChargerType chargerType);

        /// <summary>
        /// Asynchronously updates an existing charger type (for admin users).
        /// </summary>
        /// <param name="chargerType">The charger type to update.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> UpdateChargerTypeAsync(ChargerType chargerType);
    }
}
