using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityEventsRepositories
{
    public class CommunityEventsAddRepository : ICommunityEventsAddRepository
    {
        private readonly ShawahinDbContext _context;

        public CommunityEventsAddRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> AddAsync(CommunityEvents events)
        {
            if (events == null)
            {
                throw new ArgumentNullException(nameof(events));
            }

            // Add the new Community Event to the database.
            _context.CommunityEvents.Add(events);
            await _context.SaveChangesAsync();

            // You can return a ResultDto with success or any additional data.
            var result = new ResultDto { Succeeded = true, Message = "Community Event added successfully" };
            return result;
        }
    }
}
