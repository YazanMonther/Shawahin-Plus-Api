using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityPostRepositories
{
    /// <summary>
    /// Repository for updating an existing Community Post.
    /// </summary>
    public interface ICommunityPostsUpdateRepository
    {
        /// <summary>
        /// Update an existing Community Post.
        /// </summary>
        Task UpdateAsync(CommunityPosts post);
    }
}
