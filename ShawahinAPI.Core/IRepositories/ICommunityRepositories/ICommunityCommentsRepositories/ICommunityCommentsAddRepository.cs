using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityCommentsRepositories
{
    /// <summary>
    /// Repository for adding a new Community Comment.
    /// </summary>
    public interface ICommunityCommentsAddRepository
    {
        /// <summary>
        /// Add a new Community Comment.
        /// </summary>
        Task AddAsync(CommunityComments comment);
    }
}
