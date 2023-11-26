using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IChargingStationsServices
{
    /// <summary>
    /// Interface for managing user favorite charging stations.
    /// </summary>
    public interface IFavoriteStationsService
    {
        /// <summary>
        /// Adds a charging station to a user's list of favorites.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="stationId">The ID of the charging station.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the success or failure of the operation.</returns>
        Task<ResultDto?> AddStationToFavoritesAsync(Guid? userId, Guid? stationId);

        /// <summary>
        /// Gets a list of favorite charging stations for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="ChargingStationDto"/>.</returns>
        Task<IEnumerable<ChargingStationDto?>> GetFavoriteStationsAsync(Guid? userId);

        /// <summary>
        /// Removes a charging station from a user's list of favorites.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="stationId">The ID of the charging station.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the success or failure of the operation.</returns>
        Task<ResultDto?> RemoveStationFromFavoritesAsync(Guid? userId, Guid? stationId);

        /// <summary>
        /// Checks if a charging station is in a user's list of favorites.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="stationId">The ID of the charging station.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating whether the station is in favorites.</returns>
        Task<bool> IsStationInFavoritesAsync(Guid? userId, Guid? stationId);
    }
}
