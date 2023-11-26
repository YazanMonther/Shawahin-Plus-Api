using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories
{
    /// <summary>
    /// Repository for updating an existing Community Event.
    /// </summary>
    public interface ICommunityEventsUpdateRepository
    {
        /// <summary>
        /// Update an existing Community Event.
        /// </summary>
        Task UpdateAsync(CommunityEvents events);
    }
}
