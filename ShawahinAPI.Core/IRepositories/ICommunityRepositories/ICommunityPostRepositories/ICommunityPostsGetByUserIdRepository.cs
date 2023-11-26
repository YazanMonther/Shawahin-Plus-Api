using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityPostRepositories
{
    /// <summary>
    /// Repository for retrieving Community Posts by user ID.
    /// </summary>
    public interface ICommunityPostsGetByUserIdRepository
    {
        /// <summary>
        /// Get Community Posts by user ID.
        /// </summary>
        Task<IEnumerable<CommunityPosts>> GetByUserIdAsync(Guid userId);
    }
}
