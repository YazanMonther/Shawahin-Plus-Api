using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories
{
    /// <summary>
    /// Repository for user login.
    /// </summary>
    public interface IUserLoginRepository
    {
        /// <summary>
        /// Log in a user using their email and password.
        /// </summary>
        /// <param name="userLoginDto"> Contains User Login Info</param>
        /// <returns>The logged-in user if successful, otherwise null.</returns>
        Task<ApplicationUser> LoginAsync(UserLoginDto userLoginDto);
    }
}
