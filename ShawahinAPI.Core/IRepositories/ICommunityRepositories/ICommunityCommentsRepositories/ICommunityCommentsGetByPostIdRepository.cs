using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityCommentsRepositories
{
    /// <summary>
    /// Repository for retrieving Community Comments for a specific Community Post.
    /// </summary>
    public interface ICommunityCommentsGetByPostIdRepository
    {
        /// <summary>
        /// Get Community Comments associated with a specific Community Post by its ID.
        /// </summary>
        Task<IEnumerable<CommunityComments>> GetByPostIdAsync(Guid postId);
    }
}
