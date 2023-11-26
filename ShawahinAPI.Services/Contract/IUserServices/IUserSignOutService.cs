using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IUserServices
{
    public interface IUserSignOutService
    {
        Task SignOutAsync();
    }
}
