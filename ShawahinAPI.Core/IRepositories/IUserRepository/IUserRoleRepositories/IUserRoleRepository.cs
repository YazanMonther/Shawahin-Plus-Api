using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository.IUserRoleRepositories
{
    /// <summary>
    /// Repository for managing user roles.
    /// </summary>
    public interface IUserRoleRepository
    {
        /// <summary>
        /// Add a role to a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="role">The user role to add.</param>
        Task AddUserRoleAsync(Guid userId, UserRole role);

        /// <summary>
        /// Remove a role from a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="role">The user role to remove.</param>
        Task RemoveUserRoleAsync(Guid userId, UserRole role);

        /// <summary>
        /// Check if a user has a specific role.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="role">The user role to check for.</param>
        Task<bool> UserHasRoleAsync(Guid userId, UserRole role);
    }
}
