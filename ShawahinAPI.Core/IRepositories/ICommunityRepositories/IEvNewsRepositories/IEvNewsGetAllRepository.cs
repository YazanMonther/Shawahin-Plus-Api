using ShawahinAPI.Core.Entities.CummunityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.IEvNewsRepositories
{
    /// <summary>
    /// Repository for managing Community EvNews.
    /// </summary>
    public interface IEvNewsGetAllRepository
    {

        /// <summary>
        /// Get all Community EvNews.
        /// </summary>
        Task<IEnumerable<CommunityEvNews>> GetAllNewsAsync();
    }
}
