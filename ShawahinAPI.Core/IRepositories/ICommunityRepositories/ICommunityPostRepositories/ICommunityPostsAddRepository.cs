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
    /// Repository for adding a new Community Post.
    /// </summary>
    public interface ICommunityPostsAddRepository
    {
        /// <summary>
        /// Add a new Community Post.
        /// </summary>
        Task<ResultDto> AddAsync(CommunityPosts post);
    }
}
