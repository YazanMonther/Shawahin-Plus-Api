using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.Entities.CummunityEntities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.IEvNewsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityEvNewsRepositories
{
    public class EvNewsGetAllRepository : IEvNewsGetAllRepository
    {
        private readonly ShawahinDbContext _context;

        public EvNewsGetAllRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommunityEvNews>> GetAllNewsAsync()
        {
            try
            {
                // Use Entity Framework Core to query the EV News from the database
                var evNews = await _context.EvNews.ToListAsync();

                return evNews;
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as database connection issues
                // You can log the exception or handle it as needed
                throw new Exception($"Failed to retrieve EV News: {ex.Message}");
            }
        }
    }
}
