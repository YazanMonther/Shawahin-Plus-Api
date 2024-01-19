using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteStationsController : ControllerBase
    {
        private readonly IFavoriteStationsService _favoriteStationsService;

        public FavoriteStationsController(IFavoriteStationsService favoriteStationsService)
        {
            _favoriteStationsService = favoriteStationsService ?? throw new ArgumentNullException(nameof(favoriteStationsService));
        }

        [HttpPost("AddToFavorite")]
        public async Task<ActionResult<ResultDto>> AddStationToFavoritesAsync(Guid? userId, Guid? stationId)
        {
            try
            {
                var result = await _favoriteStationsService.AddStationToFavoritesAsync(userId, stationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetFavorites")]
        public async Task<ActionResult<IEnumerable<ChargingStationDto>>> GetFavoriteStationsAsync(Guid? userId)
        {
            try
            {
                var stations = await _favoriteStationsService.GetFavoriteStationsAsync(userId);
                return Ok(stations);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("IsInFavorites")]
        public async Task<ActionResult<bool>> IsStationInFavoritesAsync(Guid? userId, Guid? stationId)
        {
            try
            {
                var isInFavorites = await _favoriteStationsService.IsStationInFavoritesAsync(userId, stationId);
                return Ok(isInFavorites);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("RemoveFromFavorites")]
        public async Task<ActionResult<ResultDto>> RemoveStationFromFavoritesAsync(Guid? userId, Guid? stationId)
        {
            try
            {
                var result = await _favoriteStationsService.RemoveStationFromFavoritesAsync(userId, stationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
