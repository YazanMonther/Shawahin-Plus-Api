using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityCommentsRepositories
{
    /// <summary>
    /// Repository for removing a Community Comment.
    /// </summary>
    public interface ICommunityCommentsRemoveRepository
    {
        /// <summary>
        /// Remove a Community Comment.
        /// </summary>
        Task RemoveAsync(CommunityComments comment);
    }
}
