using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories
{
    public interface IUserSignOutRepository
    {
        Task SignOutAsync();
    }
}
