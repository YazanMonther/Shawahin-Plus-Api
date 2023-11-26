using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IUserServices
{
    /// <summary>
    /// Service for user-related operations.
    /// </summary>
    public interface IUserRegistrationService
    {
        /// <summary>
        /// Register a new user account.
        /// </summary>
        /// <param name="registrationDto">User registration data.</param>
        /// <returns>A result indicating success or failure and an optional error message.</returns>
        Task<ResultDto> RegisterAsync(UserRegistrationDto registrationDto);
    }
}
