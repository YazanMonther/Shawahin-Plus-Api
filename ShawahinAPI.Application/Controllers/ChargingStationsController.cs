using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Services.Contract;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Implementation;

namespace ShawahinAPI.Application.Controllers
{
    [ApiController]
    [Route("api/chargingstations")]
    public class ChargingStationsController : ControllerBase
    {
        private readonly IChargingStationsService _chargingStationsService;
        private readonly IChargingStationRequestService _chargingStationsReqService;
        private readonly IFavoriteStationsService _userFavoriteStationsService;

        public ChargingStationsController(IChargingStationsService ChargingStationsService,
            IChargingStationRequestService ChargingStationsReqService,
            IFavoriteStationsService userFavoriteStationsGetAll)
        {
            _chargingStationsService = ChargingStationsService;
            _chargingStationsReqService = ChargingStationsReqService;
            _userFavoriteStationsService = userFavoriteStationsGetAll;
        }

        [HttpGet("getallStations")]
        public async Task<IActionResult> GetAllChargingStations()
        {
            try
            {
                var chargingStations = await _chargingStationsService.GetAllChargingStationsAsync();
                return Ok(chargingStations);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve charging stations. Please try again later. {ex}");
            }
        }

        [HttpPost("addrequest")]
        public async Task<IActionResult> AddChargingStationRequest(AddChargingStationsReqDto stationDto)
        {
            try
            {
                if (stationDto == null)
                {
                    return BadRequest($"Invalid charging station request data.");
                }

                var result = await _chargingStationsReqService.AddChargingStationRequestAsync(stationDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to add charging station request. Please try again later. {ex}");
            }
        }

        [HttpGet("getFavoriteStations/{userId}")]
        public async Task<IActionResult> GetUserFavoriteStations(Guid? userId)
        {
            try
            {
                if (userId is null)
                {
                    return BadRequest("Invalid user id");
                }

                var favoriteStations = await _userFavoriteStationsService.GetFavoriteStationsAsync(userId);
                return Ok(favoriteStations);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve user favorite stations. Please try again later. {ex}");
            }
        }

        [HttpPost("addFavoriteStations")]
        public async Task<IActionResult> AddFavoriteStation(Guid? userId, Guid? stationId)
        {
            try
            {
                if (userId == null || stationId == null)
                {
                    return BadRequest("Invalid User or Station Id");
                }

                var addToFav = await _userFavoriteStationsService.AddStationToFavoritesAsync(userId, stationId);
                return Ok(addToFav);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to add station to favorites. Please try again later. {ex}");
            }
        }


        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredChargingStations(
             string? chargerType,
             string? paymentMethod,
             string? paymentType,
             string? chargerPower,
             string? chargerStatus)
        {
            try
            {
                var chargingStations = await _chargingStationsService
                    .GetFilteredChargingStations(
                    chargerType, paymentMethod, paymentType, chargerPower, chargerStatus);

                return Ok(chargingStations);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<ChargingStationDto?>>> GetChargingStationsByUserId(Guid? userId)
        {
            try
            {
                var chargingStations = await _chargingStationsService.GetChargingStationsByUserIdAsync(userId);

                if (chargingStations == null || !chargingStations.Any())
                {
                    return NotFound("No charging stations found for the specified user ID.");
                }

                return Ok(chargingStations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error : {ex}");
            }
        }
    }
}