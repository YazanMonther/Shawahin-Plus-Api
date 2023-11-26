using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories
{
    /// <summary>
    /// Repository for managing Community Events.
    /// </summary>
    public interface ICommunityEventsGetAllRepository
    {
        /// <summary>
        /// Get all Community Events.
        /// </summary>
        Task<IEnumerable<CommunityEvents>> GetAllAsync();
    }
}
