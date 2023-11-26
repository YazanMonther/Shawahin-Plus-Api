using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.ServicesRepositories.ServiceRepositories
{
    public class ServiceAddRepository : IServiceAddRepository
    {
        private readonly ShawahinDbContext _dbContext;

        public ServiceAddRepository(ShawahinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDto> AddAsync(Services service)
        {
            try
            {
                _dbContext.Services.Add(service);
                await _dbContext.SaveChangesAsync();

                return new ResultDto { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = "Failed to add service: " + ex.Message };
            }
        }
    }
}
