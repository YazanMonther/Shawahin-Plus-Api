using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityPostRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityPostRepositories
{
    public class CommunityPostsGetAllRepository : ICommunityPostsGetAllRepository
    {
        private readonly ShawahinDbContext _context;

        public CommunityPostsGetAllRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommunityPosts>> GetAllAsync()
        {
            // Retrieve all Community Posts from the database.
            return await _context.CommunityPosts.ToListAsync();
        }
    }
}
