using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IChargingStationsServices
{
    /// <summary>
    /// Interface for managing charger types.
    /// </summary>
    public interface IChargerTypeService
    {
        /// <summary>
        /// Adds a charger type.
        /// </summary>
        /// <param name="chargerType">The charger type to add.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the success or failure of the operation.</returns>
        Task<ResultDto?> AddChargerTypeAsync(ChargerType? chargerType);

        /// <summary>
        /// Gets all charger types.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="ChargerType"/>.</returns>
        Task<IEnumerable<ChargerTypeResponseDto?>> GetAllChargerTypesAsync();

        /// <summary>
        /// Gets a charger type by its ID.
        /// </summary>
        /// <param name="chargerTypeId">The ID of the charger type.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ChargerType"/>.</returns>
        Task<ChargerTypeResponseDto?> GetChargerTypeByIdAsync(Guid? chargerTypeId);

        /// <summary>
        /// Removes a charger type.
        /// </summary>
        /// <param name="chargerType">The charger type to remove.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the success or failure of the operation.</returns>
        Task<ResultDto?> RemoveChargerTypeAsync(ChargerType chargerType);

        /// <summary>
        /// Updates a charger type.
        /// </summary>
        /// <param name="chargerType">The charger type to update.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the success or failure of the operation.</returns>
        Task<ResultDto?> UpdateChargerTypeAsync(ChargerType chargerType);
    }
}
