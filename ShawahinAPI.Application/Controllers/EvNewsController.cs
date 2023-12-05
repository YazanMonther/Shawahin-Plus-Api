using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Services.Contract.ICommunityServices;
using System.Data;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvNewsController : ControllerBase
    {
        private readonly IEvNewsService _evNewsService;

        public EvNewsController(IEvNewsService evNewsService)
        {
            _evNewsService = evNewsService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                var evNews = await _evNewsService.GetAllNewsAsync();
                return Ok(evNews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetByNewsId/{newsId}")]
        public async Task<IActionResult> GetNewsById(Guid newsId)
        {
            try
            {
                var evNews = await _evNewsService.GetNewsByIdAsync(newsId);

                if (evNews == null)
                {
                    return NotFound($"EV News with ID {newsId} not found.");
                }

                return Ok(evNews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Only for admin 
        /// </summary>
        /// <param name="newsDto">news to be added></param>
        /// <returns></returns>
        ///         [Authorize(Roles = "Admin")]

        [HttpPost("Add")]
        public async Task<IActionResult> AddNews([FromBody] CommunityEvNewsBaseDto newsDto)
        {
            try
            {
                var result = await _evNewsService.AddNewsAsync(newsDto);

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
        /// <param name="newsId"></param>
        /// <param name="newsDto"></param>
        /// <returns></returns>
        [HttpPut("Update/{newsId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateNews(Guid newsId, [FromBody] CommunityEvNewsBaseDto newsDto)
        {
            try
            {
                var result = await _evNewsService.UpdateNewsAsync(newsDto, newsId);

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
        /// <param name="newsId"></param>
        /// <returns></returns>

        [HttpDelete("Remove/{newsId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RemoveNews(Guid newsId)
        {
            try
            {
                var result = await _evNewsService.RemoveNewsAsync(newsId);

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
