using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargingStationRequestController : ControllerBase
    {
        private readonly IChargingStationRequestService _chargingStationRequestService;

        public ChargingStationRequestController(IChargingStationRequestService chargingStationRequestService)
        {
            _chargingStationRequestService = chargingStationRequestService;
        }

        [HttpPost("AddRequest")]
        public async Task<IActionResult> AddChargingStationRequest([FromBody] AddChargingStationsReqDto stationDto)
        {
            try
            {
                var result = await _chargingStationRequestService.AddChargingStationRequestAsync(stationDto);

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

        [HttpGet("GetAllRequest")]
        public async Task<IActionResult> GetAllChargingStationRequests(Guid userId)
        {
            try
            {
                var stationsReq = await _chargingStationRequestService.GetAllChargingStationRequestsAsync(userId);
                return Ok(stationsReq);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetRequestById/{requestId}")]
        public async Task<IActionResult> GetChargingStationRequestById(Guid requestId)
        {
            try
            {
                var stationReq = await _chargingStationRequestService.GetChargingStationRequestByIdAsync(requestId);

                if (stationReq == null)
                {
                    return NotFound($"Charging Station Request with ID {requestId} not found.");
                }

                return Ok(stationReq);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Update/{requestId}")]
        public async Task<IActionResult> UpdateChargingStationRequestStatus(Guid requestId, RequestStatus status)
        {
            try
            {
                var result = await _chargingStationRequestService.UpdateChargingStationRequestStatusAsync(requestId, status);

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

        [HttpDelete("Delete/{requestId}")]
        public async Task<IActionResult> RemoveChargingStationRequest(Guid requestId)
        {
            try
            {
                var result = await _chargingStationRequestService.RemoveChargingStationRequestAsync(requestId);

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
    }
}
