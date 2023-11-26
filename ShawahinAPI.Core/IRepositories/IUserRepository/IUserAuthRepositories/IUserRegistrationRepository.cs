using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository
{

    /// <summary>
    /// Repository for user registration.
    /// </summary>
    public interface IUserRegistrationRepository
    {
        /// <summary>
        /// Register a new user account.
        /// </summary>
        /// <param name="user">User registration data.</param>
        Task<ResultDto> RegisterAsync(ApplicationUser user, string password);
    }
}
