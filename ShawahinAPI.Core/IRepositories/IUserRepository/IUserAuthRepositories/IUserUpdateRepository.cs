using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository
{
    /// <summary>
    /// Repository for updating user profiles.
    /// </summary>
    public interface IUserUpdateRepository
    {
        /// <summary>
        /// Update a user's profile information.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="updateDto">User profile update data.</param>
        Task UpdateUserAsync(Guid userId, UserProfileUpdateDto updateDto);
    }
}
