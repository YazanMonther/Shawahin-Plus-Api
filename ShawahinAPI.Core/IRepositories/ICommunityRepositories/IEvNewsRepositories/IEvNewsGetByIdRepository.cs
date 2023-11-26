using ShawahinAPI.Core.Entities.CummunityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.IEvNewsRepositories
{
    public interface IEvNewsGetByIdRepository
    {
        Task<CommunityEvNews> GetNewsByIdAsync(Guid newsId);
    }
}
