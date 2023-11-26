using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository.IUserPasswordRepositories
{
    /// <summary>
    /// Repository for changing user passwords.
    /// </summary>
    public interface IUserChangePasswordRepository
    {
        /// <summary>
        /// Change a user's password.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="passwordDto">Data for changing the password.</param>
        Task<ResultDto> ChangePasswordAsync(Guid userId, ChangePasswordDto passwordDto);
    }
}
