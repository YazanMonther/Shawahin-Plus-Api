using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityPostRepositories
{
    /// <summary>
    /// Repository for retrieving a Community Post by ID.
    /// </summary>
    public interface ICommunityPostsGetByIdRepository
    {
        /// <summary>
        /// Get a Community Post by ID.
        /// </summary>
        Task<CommunityPosts> GetByIdAsync(Guid postId);
    }

}
