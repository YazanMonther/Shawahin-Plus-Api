using Microsoft.AspNetCore.Identity;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.UserRepositories.UserAuthRepositories
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserLoginRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> LoginAsync(UserLoginDto userLoginDto)
        {
            if (userLoginDto.Email == null)
            {
                throw new ArgumentNullException("Invalid Email  Address");
            }

            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user == null)
            {
                throw new ArgumentNullException("Invalid Email  Address");
            }

            var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return user; // Login successful.
            }

            throw new InvalidOperationException("Invalid Password");
        }
    }
}
