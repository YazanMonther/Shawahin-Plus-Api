using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityCommentsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityCommentsRepositories
{
    public class CommunityCommentsAddRepository : ICommunityCommentsAddRepository
    {
        private readonly ShawahinDbContext _context;

        public CommunityCommentsAddRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CommunityComments comment)
        {
            _context.CommunityComments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}
