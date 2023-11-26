using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IUserServices
{
    /// <summary>
    /// Service for user profile management.
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// Get user profile information.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>User profile information if the user exists; otherwise, null.</returns>
        Task<UserProfileDto> GetUserProfileAsync(string userId);
    }
}
