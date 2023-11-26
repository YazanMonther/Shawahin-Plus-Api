using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository.IUserPasswordRepositories
{
    /// <summary>
    /// Repository for resetting user passwords.
    /// </summary>
    public interface IUserResetPasswordRepository
    {
        /// <summary>
        /// Reset a user's password.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="newPassword">The new password to set.</param>
        Task<ResultDto> ResetPasswordAsync(string email, string newPassword);
    }
}
