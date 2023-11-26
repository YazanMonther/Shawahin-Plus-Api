using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityEventsRepositories
{
    public class CommunityEventsRemoveRepository : ICommunityEventsRemoveRepository
    {
        private readonly ShawahinDbContext _context;

        public CommunityEventsRemoveRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task RemoveAsync(CommunityEvents events)
        {
            // Find the Community Event in the database and remove it.
            _context.CommunityEvents.Remove(events);
            await _context.SaveChangesAsync();
        }
    }
}
