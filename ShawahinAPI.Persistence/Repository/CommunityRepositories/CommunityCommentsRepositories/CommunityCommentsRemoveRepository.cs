using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityCommentsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityCommentsRepositories
{
    public class CommunityCommentsRemoveRepository : ICommunityCommentsRemoveRepository
    {
        private readonly ShawahinDbContext _context;

        public CommunityCommentsRemoveRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task RemoveAsync(CommunityComments comment)
        {
            // Remove the specified community comment from the database.
            _context.CommunityComments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
