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
    public class UserSignOutRepository : IUserSignOutRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserSignOutRepository(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task SignOutAsync()
        {
          await  _signInManager.SignOutAsync();  
        }
    }
}
