using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories
{
    /// <summary>
    /// Repository for retrieving a Community Event by ID.
    /// </summary>
    public interface ICommunityEventsGetByIdRepository
    {
        /// <summary>
        /// Get a Community Event by ID.
        /// </summary>
        Task<CommunityEvents> GetByIdAsync(Guid eventId);
    }
}
