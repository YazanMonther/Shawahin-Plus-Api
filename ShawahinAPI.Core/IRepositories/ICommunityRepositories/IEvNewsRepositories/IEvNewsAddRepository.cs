using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.CummunityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.IEvNewsRepositories
{
    /// <summary>
    /// Repository for adding a new EV News.
    /// </summary>
    public interface IEvNewsAddRepository
    {
        /// <summary>
        /// Add a new EV News.
        /// </summary>
        /// <param name="news">The EV News to add.</param>
        /// <returns>A ResultDto indicating the result of the operation.</returns>
        Task<ResultDto> AddNewsAsync(CommunityEvNews news);
    }
}
