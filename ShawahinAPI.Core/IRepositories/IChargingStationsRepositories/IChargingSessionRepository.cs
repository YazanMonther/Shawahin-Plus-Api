using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{
    /// <summary>
    /// Repository interface for managing charging sessions.
    /// </summary>
    public interface IChargingSessionRepository
    {
        /// <summary>
        /// Add a new charging session asynchronously.
        /// </summary>
        /// <param name="session">The charging session to add.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> AddSessionAsync(ChargingSessions session);

        /// <summary>
        /// Get all charging sessions asynchronously.
        /// </summary>
        /// <returns>A collection of all charging sessions.</returns>
        Task<IEnumerable<ChargingSessions>> GetAllSessionsAsync();

        /// <summary>
        /// Get a charging session by ID asynchronously.
        /// </summary>
        /// <param name="sessionId">The ID of the charging session to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the charging session with the specified ID.</returns>
        Task<ChargingSessions> GetSessionByIdAsync(Guid sessionId);

        /// <summary>
        /// Remove a charging session asynchronously.
        /// </summary>
        /// <param name="session">The charging session to remove.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> RemoveSessionAsync(ChargingSessions session);

        /// <summary>
        /// Update an existing charging session asynchronously.
        /// </summary>
        /// <param name="session">The charging session to update.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> UpdateSessionAsync(ChargingSessions session);

        /// <summary>
        /// Get all charging sessions for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve sessions for.</param>
        /// <returns>A collection of charging sessions for the specified user.</returns>
        Task<IEnumerable<ChargingSessions>> GetSessionsByUserIdAsync(Guid userId);
    }

}
