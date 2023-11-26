using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityPostRepositories
{
    /// <summary>
    /// Repository for managing Community Posts.
    /// </summary>
    public interface ICommunityPostsGetAllRepository
    {
        /// <summary>
        /// Get all Community Posts.
        /// </summary>
        Task<IEnumerable<CommunityPosts>> GetAllAsync();
    }
}
