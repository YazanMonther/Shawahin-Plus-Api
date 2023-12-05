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
    /// Service interface for managing community EV news.
    /// </summary>
    public interface IEvNewsService
    {
        /// <summary>
        /// Adds a new community EV news item.
        /// </summary>
        /// <param name="newsDto">The community EV news DTO to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> AddNewsAsync(CommunityEvNewsBaseDto? newsDto);

        /// <summary>
        /// Gets all community EV news items.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a collection of community EV news items.</returns>
        Task<IEnumerable<CommunityEvNewsResponseDto?>> GetAllNewsAsync();

        /// <summary>
        /// Gets a community EV news item by ID.
        /// </summary>
        /// <param name="newsId">The ID of the community EV news item to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the specified community EV news item.</returns>
        Task<CommunityEvNewsResponseDto?> GetNewsByIdAsync(Guid newsId);

        /// <summary>
        /// Removes a community EV news item.
        /// </summary>
        /// <param name="newsId">The ID of the community EV news item to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> RemoveNewsAsync(Guid newsId);

        /// <summary>
        /// Updates an existing community EV news item.
        /// </summary>
        /// <param name="newsDto">The updated community EV news DTO.</param>
        /// <param name="newsId">The ID of the community EV news item to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> UpdateNewsAsync(CommunityEvNewsBaseDto? newsDto, Guid newsId);
    }
}
