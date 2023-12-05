using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Application.Controllers
{
    /// <summary>
    /// Controller for managing favorite charging stations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteStationsController : ControllerBase
    {
        private readonly IFavoriteStationsService _favoriteStationsService;

        /// <summary>
        /// Constructor for the FavoriteStationsController.
        /// </summary>
        /// <param name="favoriteStationsService">The service for managing favorite charging stations.</param>
        public FavoriteStationsController(IFavoriteStationsService favoriteStationsService)
        {
            _favoriteStationsService = favoriteStationsService ?? throw new ArgumentNullException(nameof(favoriteStationsService));
        }

        /// <summary>
        /// Adds a charging station to the user's favorite list.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="stationId">The ID of the charging station.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpPost("AddToFavorite")]
        public async Task<ActionResult<ResultDto>> AddStationToFavoritesAsync(Guid? userId, Guid? stationId)
        {
            var result = await _favoriteStationsService.AddStationToFavoritesAsync(userId, stationId);
            return Ok(result);
        }

        /// <summary>
        /// Gets the list of favorite charging stations for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The list of favorite charging stations.</returns>
        [HttpGet("GetFavorites")]
        public async Task<ActionResult<IEnumerable<ChargingStationDto>>> GetFavoriteStationsAsync(Guid? userId)
        {
            var stations = await _favoriteStationsService.GetFavoriteStationsAsync(userId);
            return Ok(stations);
        }

        /// <summary>
        /// Checks if a charging station is in the user's favorites.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="stationId">The ID of the charging station.</param>
        /// <returns>True if the station is in favorites; otherwise, false.</returns>
        [HttpGet("IsInFavorites")]
        public async Task<ActionResult<bool>> IsStationInFavoritesAsync(Guid? userId, Guid? stationId)
        {
            var isInFavorites = await _favoriteStationsService.IsStationInFavoritesAsync(userId, stationId);
            return Ok(isInFavorites);
        }

        /// <summary>
        /// Removes a charging station from the user's favorite list.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="stationId">The ID of the charging station.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpPost("RemoveFromFavorites")]
        public async Task<ActionResult<ResultDto>> RemoveStationFromFavoritesAsync(Guid? userId, Guid? stationId)
        {
            var result = await _favoriteStationsService.RemoveStationFromFavoritesAsync(userId, stationId);
            return Ok(result);
        }
    }
}
