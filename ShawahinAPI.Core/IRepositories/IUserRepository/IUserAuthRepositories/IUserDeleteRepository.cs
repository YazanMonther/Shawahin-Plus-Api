using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IUserRepository
{
    /// <summary>
    /// Repository for deleting user accounts.
    /// </summary>
    public interface IUserDeleteRepository
    {
        /// <summary>
        /// Delete a user account.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete.</param>
        Task DeleteUserAsync(Guid userId);
    }
}
