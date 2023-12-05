using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Application.Controllers
{
    [ApiController]
    [Route("api/chargingstationcomments")]
    [Authorize] // Assuming authentication is required to access this controller

    public class ChargerStationCommentsController : ControllerBase
    {
        private readonly IChargerStationCommentsService _chargerStationCommentsService;

        public ChargerStationCommentsController(IChargerStationCommentsService chargerStationCommentsService)
        {
            _chargerStationCommentsService = chargerStationCommentsService;
        }

        [HttpPost("addComment")]
        public async Task<IActionResult> AddComment([FromBody] ChargerStationCommentBaseDto commentDto)
        {
            try
            {
                var result = await _chargerStationCommentsService.AddCommentAsync(commentDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to add comment. {ex.Message}");
            }
        }

        [HttpGet("getCommentsForStation/{stationId}")]
        public async Task<IActionResult> GetCommentsForStation(Guid stationId)
        {
            try
            {
                var comments = await _chargerStationCommentsService.GetCommentsForStationAsync(stationId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve comments for the station. {ex.Message}");
            }
        }

        [HttpDelete("removeComment/{commentId}")]
        public async Task<IActionResult> RemoveComment(Guid commentId)
        {
            try
            {
                var result = await _chargerStationCommentsService.RemoveCommentAsync(commentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to remove comment. {ex.Message}");
            }
        }

        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComment([FromBody] ChargerStationCommentResponeDto commentDto)
        {
            try
            {
                var result = await _chargerStationCommentsService.UpdateCommentAsync(commentDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to update comment. {ex.Message}");
            }
        }

        [HttpGet("getCommentById/{commentId}")]
        public async Task<IActionResult> GetCommentById(Guid commentId)
        {
            try
            {
                var comment = await _chargerStationCommentsService.GetCommentByIdAsync(commentId);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve comment by ID. {ex.Message}");
            }
        }
    }

}
