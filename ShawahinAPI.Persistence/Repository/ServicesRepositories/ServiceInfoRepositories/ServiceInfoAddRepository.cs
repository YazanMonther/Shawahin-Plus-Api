using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.IRepositories.IServiceRepositories.IServiceInfoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.ServicesRepositories.ServiceInfoRepositories
{
    public class ServiceInfoAddRepository : IServiceInfoAddRepository
    {
        private readonly ShawahinDbContext _dbContext;

        public ServiceInfoAddRepository(ShawahinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDto> AddAsync(ServiceInfo serviceInfo)
        {
            try
            {
                _dbContext.ServiceInfo.Add(serviceInfo);
                await _dbContext.SaveChangesAsync();

                return new ResultDto
                {
                    Succeeded = true,
                };
            }
            catch (Exception ex)
            {
                // Handle any exceptions here and provide an appropriate error message.
                return new ResultDto
                {
                    Succeeded = false,
                    Message = $"Error: {ex.Message}",
                };
            }
        }
    }
}
