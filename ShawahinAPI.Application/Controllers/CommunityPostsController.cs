using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Services.Contract.ICommunityServices;

namespace ShawahinAPI.Application.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class CommunityPostsController : ControllerBase
        {
            private readonly ICommunityPostsService _communityPostsService;

            public CommunityPostsController(ICommunityPostsService communityPostsService)
            {
                _communityPostsService = communityPostsService;
            }

            [HttpGet("GetAll")]
            public async Task<IActionResult> GetAllPosts()
            {
                try
                {
                    var posts = await _communityPostsService.GetAllPostsAsync();
                    return Ok(posts);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpGet("GetById/{id}")]
            public async Task<IActionResult> GetPostById(Guid id)
            {
                try
                {
                    var communityPost = await _communityPostsService.GetPostByIdAsync(id);

                    if (communityPost == null)
                    {
                        return NotFound($"Community Post with ID {id} not found.");
                    }

                    return Ok(communityPost);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpGet("GetByUserId/{userId}")]
            public async Task<IActionResult> GetPostsByUserId(Guid userId)
            {
                try
                {
                    var posts = await _communityPostsService.GetPostsByUserIdAsync(userId);
                    return Ok(posts);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpPost("Add")]
            public async Task<IActionResult> AddPost([FromBody] CommunityPostBaseDto postDto)
            {
                try
                {
                    var result = await _communityPostsService.AddPostAsync(postDto);

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

            [HttpPut("Update/{id}")]
            public async Task<IActionResult> UpdatePost(Guid id, [FromBody] CommunityPostBaseDto postDto)
            {
                try
                {
                    var result = await _communityPostsService.UpdatePostAsync(postDto, id);

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

            [HttpDelete("Remove/{id}")]
            public async Task<IActionResult> RemovePost(Guid id)
            {
                try
                {
                    var result = await _communityPostsService.RemovePostAsync(id);

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
