using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.CummunityEntities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.IEvNewsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityEvNewsRepositories
{
    public class EvNewsAddRepository : IEvNewsAddRepository
    {
        private readonly ShawahinDbContext _context;

        public EvNewsAddRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> AddNewsAsync(CommunityEvNews news)
        {
            try
            {
                // Add the EV News to the context
                _context.EvNews.Add(news);
                await _context.SaveChangesAsync();

                // You can return a success ResultDto
                return new ResultDto
                {
                    Succeeded = true,
                    Message = "EV News added successfully."
                };
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a failure ResultDto
                return new ResultDto
                {
                    Succeeded = false,
                    Message = $"Failed to add EV News: {ex.Message}"
                };
            }
        }
    }
}
