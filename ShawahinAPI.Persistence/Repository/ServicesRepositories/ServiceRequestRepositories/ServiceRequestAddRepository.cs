using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.IRepositories.IServicesRepositories.IServiceRequestRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.ServicesRepositories.ServiceRequestRepositories
{
    public class ServiceRequestAddRepository : IServiceRequestAddRepository
    {
        private readonly ShawahinDbContext _dbContext;

        public ServiceRequestAddRepository(ShawahinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDto> AddAsync(ServiceRequest serviceRequest)
        {

            if(serviceRequest == null)
            {
                return new ResultDto() { Message = "Invalid service request ", Succeeded = false };
            }
            _dbContext.ServiceReq.Add(serviceRequest);
            await _dbContext.SaveChangesAsync();

            return new ResultDto() {  Succeeded = true };
        }
    }
}
