using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{
    /// <summary>
    /// Repository interface for operations on <see cref="ChargerStationComments"/> entities.
    /// </summary>
    public interface IChargerStationCommentsRepository
    {
        /// <summary>
        /// Add a comment on the charger station asynchronously.
        /// </summary>
        /// <param name="comment">The charger station comment to add.</param>
        /// <returns>A ResultDto indicating the result of the operation.</returns>
        Task<ResultDto> AddCommentAsync(ChargerStationComments comment);

        /// <summary>
        /// Get all comments for a specific charger station asynchronously.
        /// </summary>
        /// <param name="stationId">The ID of the station to retrieve comments for.</param>
        /// <returns>A list of comments for the specified station.</returns>
        Task<IEnumerable<ChargerStationComments>> GetCommentsForStationAsync(Guid stationId);

        /// <summary>
        /// Remove a comment on the charger station asynchronously.
        /// </summary>
        /// <param name="comment">The charger station comment to remove.</param>
        /// <returns>A ResultDto indicating the result of the operation.</returns>
        Task<ResultDto> RemoveCommentAsync(ChargerStationComments comment);

        /// <summary>
        /// Additional method 1: Update a comment on the charger station asynchronously.
        /// </summary>
        /// <param name="comment">The charger station comment to update.</param>
        /// <returns>A ResultDto indicating the result of the operation.</returns>
        Task<ResultDto> UpdateCommentAsync(ChargerStationComments comment);

        /// <summary>
        /// Additional method 2: Get a comment by its ID asynchronously.
        /// </summary>
        /// <param name="commentId">The ID of the comment to retrieve.</param>
        /// <returns>The charger station comment with the specified ID.</returns>
        Task<ChargerStationComments?> GetCommentByIdAsync(Guid commentId);
    }

}
