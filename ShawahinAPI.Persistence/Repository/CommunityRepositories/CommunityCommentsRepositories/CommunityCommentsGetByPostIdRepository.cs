using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityCommentsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityCommentsRepositories
{
    public class CommunityCommentsGetByPostIdRepository : ICommunityCommentsGetByPostIdRepository
    {
        private readonly ShawahinDbContext _context;

        public CommunityCommentsGetByPostIdRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommunityComments>> GetByPostIdAsync(Guid postId)
        {
            // Retrieve all community comments associated with the specified post ID.
            var comments = await _context.CommunityComments
                .Where(c => c.PostId == postId)
                .ToListAsync(); 

            return comments;
        }
    }
}
