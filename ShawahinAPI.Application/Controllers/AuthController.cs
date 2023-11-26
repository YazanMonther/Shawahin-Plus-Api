using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Services.Contract.IUserServices;

namespace ShawahinAPI.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserRegistrationService _registrationService;
        private readonly IUserSignOutService _signOutService;
        public AuthController(IUserAuthenticationService authenticationService, 
            IUserRegistrationService registrationService,
            IUserSignOutService signOutService)
        {
            _authenticationService = authenticationService;
            _registrationService = registrationService;
            _signOutService = signOutService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var result = await _authenticationService.AuthenticateAsync(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            try
            {
                var result = await _registrationService.RegisterAsync(registrationDto);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Registration successful" });
                }
                else
                {
                    return BadRequest(new { errors = result.Message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> Sign_Out()
        {
            await _signOutService.SignOutAsync();
            return Ok("User signed out successfully.");
        }
    }
}
