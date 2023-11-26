using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityEventsRepositories
{
    public class CommunityEventsGetAllRepository : ICommunityEventsGetAllRepository
    {
        private readonly ShawahinDbContext _context;

        public CommunityEventsGetAllRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommunityEvents>> GetAllAsync()
        {
            // Retrieve all Community Events from the database.
            var events = await _context.CommunityEvents.ToListAsync();
            return events;
        }
    }
}
