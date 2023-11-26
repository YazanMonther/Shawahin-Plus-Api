using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{
    /// <summary>
    /// Repository interface for managing a user's favorite charging stations.
    /// </summary>
    public interface IUserFavoriteStationsRepository
    {
        /// <summary>
        /// Asynchronously adds a charging station to a user's favorite stations.
        /// </summary>
        /// <param name="user">The user to add the favorite station for.</param>
        /// <param name="station">The charging station to add to favorites.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> AddStationToFavoritesAsync(ApplicationUser? user, ChargingStations? station);

        /// <summary>
        /// Asynchronously retrieves all favorite charging stations for a user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve favorite stations for.</param>
        /// <returns>A collection of favorite charging stations for the specified user.</returns>
        Task<IEnumerable<ChargingStations?>> GetFavoriteStationsAsync(Guid? userId);

        /// <summary>
        /// Asynchronously removes a charging station from a user's favorite stations.
        /// </summary>
        /// <param name="userId">The ID of the user to remove the favorite station for.</param>
        /// <param name="stationId">The ID of the charging station to remove from favorites.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> RemoveStationFromFavoritesAsync(Guid? userId, Guid? stationId);

        /// <summary>
        /// Asynchronously checks if a charging station is in a user's favorite stations.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <param name="stationId">The ID of the charging station to check.</param>
        /// <returns>A task representing the asynchronous operation with a boolean indicating whether the station is a favorite.</returns>
        Task<bool> IsStationInFavoritesAsync(Guid? userId, Guid? stationId);
    }

}
