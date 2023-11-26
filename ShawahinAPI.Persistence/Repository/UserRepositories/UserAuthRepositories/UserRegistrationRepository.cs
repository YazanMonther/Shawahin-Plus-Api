using Microsoft.AspNetCore.Identity;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IUserRepository;
using ShawahinAPI.Core.Mappers.UserMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.UserRepositories.UserAuthRepositories
{

    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRegistrationRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultDto> RegisterAsync(ApplicationUser user ,string password)
        {

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, user.UserRole.ToString());
                return new ResultDto { Succeeded = true, };
            }
            else
            {
                // Extract error messages from IdentityResult.
                var errorMessage = string.Join(Environment.NewLine, result.Errors.Select(error => error.Description));
                return new ResultDto { Succeeded = false, Message = errorMessage };
            }
        }
    }
}
