using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;


namespace ShawahinAPI.Services.Contract.IChargingStationsServices
{
    /// <summary>
    /// Service interface for managing charging station comments.
    /// </summary>
    public interface IChargerStationCommentsService
    {
        /// <summary>
        /// Adds a new comment to a charging station.
        /// </summary>
        /// <param name="commentDto">The DTO containing comment information.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the success or failure of the operation.</returns>
        Task<ResultDto> AddCommentAsync(ChargerStationCommentBaseDto commentDto);

        /// <summary>
        /// Retrieves all comments for a specific charging station.
        /// </summary>
        /// <param name="stationId">The ID of the charging station.</param>
        /// <returns>An enumerable of <see cref="ChargerStationCommentResponeDto"/> representing the comments.</returns>
        Task<IEnumerable<ChargerStationCommentResponeDto>> GetCommentsForStationAsync(Guid stationId);

        /// <summary>
        /// Removes a comment from a charging station.
        /// </summary>
        /// <param name="commentId">The ID of the comment to be removed.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the success or failure of the operation.</returns>
        Task<ResultDto> RemoveCommentAsync(Guid commentId);

        /// <summary>
        /// Updates an existing comment on a charging station.
        /// </summary>
        /// <param name="commentDto">The DTO containing updated comment information.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the success or failure of the operation.</returns>
        Task<ResultDto> UpdateCommentAsync(ChargerStationCommentResponeDto commentDto);

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment to be retrieved.</param>
        /// <returns>A <see cref="ChargerStationCommentBaseDto"/> representing the comment, or null if not found.</returns>
        Task<ChargerStationCommentBaseDto?> GetCommentByIdAsync(Guid commentId);
    }
}
