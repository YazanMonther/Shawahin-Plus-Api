using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IUserServices
{

    /// <summary>
    /// Service for user authentication.
    /// </summary>
    public interface IUserAuthenticationService
    {

        /// <summary>
        /// Authenticate a user and generate an access token.
        /// </summary>
        /// <param name="loginDto">User login data.</param>
        /// <returns>An access token and user info  if authentication is successful; otherwise, an error result.</returns>
        Task<UserAuthenticationResultDto> AuthenticateAsync(UserLoginDto loginDto);
    }

}
