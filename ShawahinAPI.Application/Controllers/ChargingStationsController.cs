using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using System.Data;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargingStationsController : ControllerBase
    {
        private readonly IChargingStationsService _chargingStationsService;

        public ChargingStationsController(IChargingStationsService chargingStationsService)
        {
            _chargingStationsService = chargingStationsService;
        }


        /// <summary>
        /// only for Admin
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{requestId}/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewChargingStation(Guid requestId, Guid userId)
        {
            try
            {
                var result = await _chargingStationsService.AddNewChargingStationAsync(requestId, userId);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllChargingStations()
        {
            try
            {
                var chargingStations = await _chargingStationsService.GetAllChargingStationsAsync();
                return Ok(chargingStations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetbyId/{stationId}")]
        public async Task<IActionResult> GetChargingStationById(Guid stationId)
        {
            try
            {
                var chargingStation = await _chargingStationsService.GetChargingStationByIdAsync(stationId);

                if (chargingStation == null)
                {
                    return NotFound($"Charging Station with ID {stationId} not found.");
                }

                return Ok(chargingStation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update/{stationId}")]
        public async Task<IActionResult> UpdateChargingStation(Guid stationId, ChargingStationDto updatedStation)
        {
            try
            {
                var result = await _chargingStationsService.UpdateChargingStationAsync(stationId, updatedStation);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Remove/{stationId}")]
        public async Task<IActionResult> RemoveChargingStation(Guid stationId)
        {
            try
            {
                var result = await _chargingStationsService.RemoveChargingStationAsync(stationId);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredChargingStations(
            [FromQuery] string? chargerType,
            [FromQuery] string? paymentMethod,
            [FromQuery] string? paymentType,
            [FromQuery] string? chargerPower,
            [FromQuery] string? chargerStatus)
        {
            try
            {
                var chargingStations = await _chargingStationsService.GetFilteredChargingStations(
                    chargerType, paymentMethod, paymentType, chargerPower, chargerStatus);

                return Ok(chargingStations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetbyUserId/{userId}")]
        public async Task<IActionResult> GetChargingStationsByUserId(Guid userId)
        {
            try
            {
                var chargingStations = await _chargingStationsService.GetChargingStationsByUserIdAsync(userId);
                return Ok(chargingStations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
