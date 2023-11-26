using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityPostRepositories
{

    /// <summary>
    /// Repository for removing a Community Post.
    /// </summary>
    public interface ICommunityPostsRemoveRepository
    {
        /// <summary>
        /// Remove a Community Post.
        /// </summary>
        Task<ResultDto> RemoveAsync(CommunityPosts post);
    }
}
