using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityCommentsRepositories
{
    /// <summary>
    /// Repository for retrieving all Community Comments.
    /// </summary>
    public interface ICommunityCommentsGetAllRepository
    {
        /// <summary>
        /// Get all Community Comments.
        /// </summary>
        Task<IEnumerable<CommunityComments>> GetAllAsync();
    }
}
