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
    public class EvNewsRemoveRepository : IEvNewsRemoveRepository
    {
        private readonly ShawahinDbContext _context;

        public EvNewsRemoveRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> RemoveNewsAsync(CommunityEvNews news)
        {
            try
            {
                // Check if the news item exists in the database
                var existingNews = await _context.EvNews.FindAsync(news.Id);
                if (existingNews == null)
                {
                    // News item not found, return an error result
                    return new ResultDto
                    {
                        Succeeded = false,
                        Message = "EV News not found.",
                    };
                }

                // Remove the news item
                _context.EvNews.Remove(existingNews);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return a success result
                return new ResultDto
                {
                    Succeeded = true,
                    Message = "EV News removed successfully.",
                };
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as database connection issues
                // You can log the exception or handle it as needed
                return new ResultDto
                {
                    Succeeded = false,
                    Message = $"Failed to remove EV News: {ex.Message}",
                };
            }
        }
    }
}
