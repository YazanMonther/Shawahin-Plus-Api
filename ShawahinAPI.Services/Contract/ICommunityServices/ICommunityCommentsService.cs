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
    /// Service interface for managing community comments.
    /// </summary>
    public interface ICommunityCommentsService
    {
        /// <summary>
        /// Adds a new community comment.
        /// </summary>
        /// <param name="commentDto">The community comment DTO to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> AddCommentAsync(CommunityCommentBaseDto commentDto);

        /// <summary>
        /// Gets all community comments.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a collection of community comments.</returns>
        Task<IEnumerable<CommunityCommentBaseDto?>> GetAllCommentsAsync();

        /// <summary>
        /// Gets community comments associated with a specific post ID.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>A task representing the asynchronous operation that returns a collection of community comments.</returns>
        Task<IEnumerable<CommunityCommentBaseDto?>> GetCommentsByPostIdAsync(Guid postId);

        /// <summary>
        /// Removes a community comment.
        /// </summary>
        /// <param name="commentId">The ID of the community comment to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> RemoveCommentAsync(Guid commentId);
    
    }
}
