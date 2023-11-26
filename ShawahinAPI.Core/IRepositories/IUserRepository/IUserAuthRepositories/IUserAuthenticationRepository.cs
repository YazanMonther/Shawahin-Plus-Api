using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories
{
    /// <summary>
    /// Repository for user authentication.
    /// </summary>
    public interface IUserAuthenticationRepository
    {
        /// <summary>
        /// Get authenticated user information by email.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        Task<AuthUserDto?> GetAuthUserAsync(string email);
    }
}
