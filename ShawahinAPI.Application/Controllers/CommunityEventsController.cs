using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Services.Contract.ICommunityServices;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityEventsController : ControllerBase
    {
        private readonly ICommunityEventsService _communityEventsService;

        public CommunityEventsController(ICommunityEventsService communityEventsService)
        {
            _communityEventsService = communityEventsService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                var events = await _communityEventsService.GetAllEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            try
            {
                var communityEvent = await _communityEventsService.GetEventByIdAsync(id);

                if (communityEvent == null)
                {
                    return NotFound($"Community Event with ID {id} not found.");
                }

                return Ok(communityEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <param name="eventDto"> event to be added </param>
        /// <returns>result message</returns>
        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEvent([FromBody] CommunityEventBaseDto eventDto)
        {
            try
            {
                var result = await _communityEventsService.AddEventAsync(eventDto);

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

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <param name="id">event Id </param>
        /// <param name="eventDto"> event to be updated</param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] CommunityEventBaseDto eventDto)
        {
            try
            {
                var result = await _communityEventsService.UpdateEventAsync(eventDto, id);

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

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <param name="id"> event id to be deleted</param>
        /// <returns>resutl message </returns>
        [HttpDelete("Remove/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveEvent(Guid id)
        {
            try
            {
                var result = await _communityEventsService.RemoveEventAsync(id);

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
