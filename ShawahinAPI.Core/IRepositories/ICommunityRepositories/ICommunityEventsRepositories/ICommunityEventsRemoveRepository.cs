using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories
{
    /// <summary>
    /// Repository for removing a Community Event.
    /// </summary>
    public interface ICommunityEventsRemoveRepository
    {
        /// <summary>
        /// Remove a Community Event.
        /// </summary>
        Task RemoveAsync(CommunityEvents events);
    }
}
