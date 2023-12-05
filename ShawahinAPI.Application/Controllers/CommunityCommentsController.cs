using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Services.Contract.ICommunityServices;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityCommentsController : ControllerBase
    {
        private readonly ICommunityCommentsService _communityCommentsService;

        public CommunityCommentsController(ICommunityCommentsService communityCommentsService)
        {
            _communityCommentsService = communityCommentsService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await _communityCommentsService.GetAllCommentsAsync();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetBypostId/{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(Guid postId)
        {
            try
            {
                var comments = await _communityCommentsService.GetCommentsByPostIdAsync(postId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddComment([FromBody] CommunityCommentBaseDto commentDto)
        {
            try
            {
                var result = await _communityCommentsService.AddCommentAsync(commentDto);

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

        [HttpDelete("Remove/{commentId}")]
        public async Task<IActionResult> RemoveComment(Guid commentId)
        {
            try
            {
                var result = await _communityCommentsService.RemoveCommentAsync(commentId);

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
