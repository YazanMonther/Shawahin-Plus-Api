using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityPostRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityPostRepositories
{
    public class CommunityPostsAddRepository : ICommunityPostsAddRepository
    {
        private readonly ShawahinDbContext _context;

        public CommunityPostsAddRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> AddAsync(CommunityPosts post)
        {
            if(post == null)
            {
                return new ResultDto() { Succeeded = false, Message = "Invalid post provider" };
            }
            _context.CommunityPosts.Add(post);
            await _context.SaveChangesAsync();

            return new ResultDto() { Succeeded = true };
        }
    }
}
