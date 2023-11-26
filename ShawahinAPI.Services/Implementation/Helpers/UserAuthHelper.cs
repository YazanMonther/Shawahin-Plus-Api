using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Implementation.Helpers
{
    public static class UserAuthHelper
    {
        public static async Task<ResultDto> ValidateUserAdminId(Guid? userId, IUserGetRepository userGetRepository)
        {
            if (userId == null || userId == Guid.Empty)
            {
                return new ResultDto { Succeeded = false, Message = "Empty User Id" };
            }

            var user = await userGetRepository.GetUserByIdAsync(userId.Value);

            if (user == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid User Id" };
            }

            if (user.UserRole != UserRole.Admin)
            {
                return new ResultDto() { Succeeded = false, Message = " User Isn`t an Admin " };
            }


            return new ResultDto { Succeeded = true, Message = "User Id is valid" };
        }
    }
}
