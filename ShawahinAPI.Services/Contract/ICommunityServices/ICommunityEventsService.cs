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
    /// Service interface for managing community events.
    /// </summary>
    public interface ICommunityEventsService
    {
        /// <summary>
        /// Adds a new community event.
        /// </summary>
        /// <param name="eventDto">The community event DTO to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> AddEventAsync(CommunityEventBaseDto? eventDto);

        /// <summary>
        /// Gets all community events.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a collection of community events.</returns>
        Task<IEnumerable<CommunityEventResponseDto?>> GetAllEventsAsync();

        /// <summary>
        /// Gets a community event by ID.
        /// </summary>
        /// <param name="eventId">The ID of the community event to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the specified community event.</returns>
        Task<CommunityEventResponseDto?> GetEventByIdAsync(Guid eventId);

        /// <summary>
        /// Removes a community event.
        /// </summary>
        /// <param name="eventId">The ID of the community event to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> RemoveEventAsync(Guid eventId);

        /// <summary>
        /// Updates an existing community event.
        /// </summary>
        /// <param name="eventDto">The updated community event DTO.</param>
        /// <param name="eventId">The event id </param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto> UpdateEventAsync(CommunityEventBaseDto? eventDto, Guid eventId);
    }
}
