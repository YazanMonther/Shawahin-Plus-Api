using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories
{
    /// <summary>
    /// Repository for adding a new Community Event.
    /// </summary>
    public interface ICommunityEventsAddRepository
    {
        /// <summary>
        /// Add a new Community Event.
        /// </summary>
        Task<ResultDto> AddAsync(CommunityEvents events);
    }
 }
