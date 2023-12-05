using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.ICommunityServices
{
    /// <summary>
    /// Service interface for managing community posts.
    /// </summary>
    public interface ICommunityPostsService
    {
        /// <summary>
        /// Adds a new community post.
        /// </summary>
        /// <param name="postDto">The community post DTO to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> AddPostAsync(CommunityPostBaseDto? postDto);

        /// <summary>
        /// Gets all community posts.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a collection of community posts.</returns>
        Task<IEnumerable<CommunityPostResponseDto?>> GetAllPostsAsync();

        /// <summary>
        /// Gets a community post by ID.
        /// </summary>
        /// <param name="postId">The ID of the community post to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the specified community post.</returns>
        Task<CommunityPostResponseDto?> GetPostByIdAsync(Guid postId);

        /// <summary>
        /// Gets community posts by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve posts for.</param>
        /// <returns>A task representing the asynchronous operation that returns a collection of community posts for the specified user.</returns>
        Task<IEnumerable<CommunityPostResponseDto?>> GetPostsByUserIdAsync(Guid userId);

        /// <summary>
        /// Removes a community post.
        /// </summary>
        /// <param name="postId">The ID of the community post to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> RemovePostAsync(Guid postId);

        /// <summary>
        /// Updates an existing community post.
        /// </summary>
        /// <param name="postDto">The updated community post DTO.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> UpdatePostAsync(CommunityPostBaseDto? postDto , Guid postId);
    }
}
