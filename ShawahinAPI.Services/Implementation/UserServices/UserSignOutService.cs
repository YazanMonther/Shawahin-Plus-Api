using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Services.Contract.IUserServices;

namespace ShawahinAPI.Services.Implementation.UserServices
{
    public class UserSignOutService : IUserSignOutService
    {
        private readonly IUserSignOutRepository _signOutRepository;

        public UserSignOutService(IUserSignOutRepository signOutRepository)
        {
            _signOutRepository = signOutRepository;
        }

        public async Task SignOutAsync()
        {
            await _signOutRepository.SignOutAsync();
        }
    }
}
