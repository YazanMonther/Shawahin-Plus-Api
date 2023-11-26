using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories
{
    /// <summary>
    /// Repository for retrieving user information.
    /// </summary>
    public interface IUserGetRepository
    {
        /// <summary>
        /// Get a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        Task<ApplicationUser?> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Get a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
    }
}
