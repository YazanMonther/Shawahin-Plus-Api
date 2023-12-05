using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Services.Contract.IServiceServices;

namespace ShawahinAPI.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteServicesController : ControllerBase
    {
        private readonly IFavoriteServicesService _favoriteServicesService;

        public FavoriteServicesController(IFavoriteServicesService favoriteServicesService)
        {
            _favoriteServicesService = favoriteServicesService ?? throw new ArgumentNullException(nameof(favoriteServicesService));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ServiceResponseDto>>> GetFavoriteServices(Guid userId)
        {
            try
            {
                var favoriteServices = await _favoriteServicesService.GetFavoriteServicesAsync(userId);
                return Ok(favoriteServices);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ResultDto>> AddServiceToFavorites([FromBody] Guid UserId,Guid ServiceId)
        {
            try
            {
                // Assuming your ServiceRequestDto contains ServiceId and UserId
                var result = await _favoriteServicesService.AddServiceToFavoritesAsync(UserId, ServiceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("remove")]
        public async Task<ActionResult<ResultDto>> RemoveServiceFromFavorites([FromBody] Guid UserId, Guid ServiceId)
        {
            try
            {
                // Assuming your ServiceRequestDto contains ServiceId and UserId
                var result = await _favoriteServicesService.RemoveServiceFromFavoritesAsync(UserId, ServiceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
