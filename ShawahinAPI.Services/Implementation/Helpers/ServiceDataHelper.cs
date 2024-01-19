using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.Entities.ServicesEntitiess;
using ShawahinAPI.Core.IRepositories;

namespace ShawahinAPI.Services.Implementation.Helpers
{
    public static class ServiceDataHelper
    {
        public async static Task<IEnumerable<ServiceRequestResponseDto>> ToResponseRequestList(
            IEnumerable<ServiceRequest> serviceRequests,
            IRepository<ServiceInfo> repositoryInfo,
            IRepository<ServiceType> repositoryType)
        {
            List<ServiceRequestResponseDto> serviceRequestResponseDtos = new List<ServiceRequestResponseDto>();

            foreach (ServiceRequest serviceRequest in serviceRequests)
            {
                var serviceInfo = await repositoryInfo.GetByIdAsync(serviceRequest.ServiceInfoId);
                var serviceType = await repositoryType.GetByIdAsync(serviceInfo?.ServiceTypeId ?? Guid.Empty);

                serviceRequestResponseDtos.Add(new ServiceRequestResponseDto()
                {
                    Id = serviceRequest.Id,
                    ServiceName = serviceInfo?.ServiceName ?? null,
                    ServiceTypeId = serviceType?.Id ?? Guid.Empty,
                    ServiceTypeName = serviceType?.ServiceTypeName ?? null,
                    PhoneNumber = serviceInfo?.PhoneNumber ?? null,
                    Description = serviceInfo?.Description ?? null,
                    UserId = serviceRequest.UserId,
                    Address = serviceInfo?.Address,
                    City = serviceInfo?.City,
                    ImageUrl = serviceInfo?.ImageUrl,
                    RequestStatus = serviceRequest?.RequestStatus
                });
            }

            return serviceRequestResponseDtos;
        }


        public async static Task<ServiceRequestResponseDto> ToResponseRequest(
         ServiceRequest serviceRequests,
         IRepository<ServiceInfo> repositoryInfo,
         IRepository<ServiceType> repositoryType)
        {
            var serviceRequestResponseDtos = new ServiceRequestResponseDto();

            var serviceInfo = await repositoryInfo.GetByIdAsync(serviceRequests.ServiceInfoId);
            var serviceType = await repositoryType.GetByIdAsync(serviceInfo?.ServiceTypeId ?? Guid.Empty);

            return new ServiceRequestResponseDto()
            {
                Id = serviceRequests.Id,
                ServiceName = serviceInfo?.ServiceName ?? null,
                ServiceTypeId = serviceInfo?.Id ?? Guid.Empty,
                ServiceTypeName = serviceType?.ServiceTypeName ?? null,
                PhoneNumber = serviceInfo?.PhoneNumber ?? null,
                Description = serviceInfo?.Description ?? null,
                UserId = serviceRequests.UserId,
                Address = serviceInfo?.Address,
                City = serviceInfo?.City,
                ImageUrl = serviceInfo?.ImageUrl,
                RequestStatus = serviceRequests?.RequestStatus
            };
        }



        public async static Task<IEnumerable<ServiceResponseDto>> ToResponseList(
            IEnumerable<ShawahinAPI.Core.Entities.ServicesEntities.Services> services
                           )
        {
            List<ServiceResponseDto> serviceResponseDtos = new List<ServiceResponseDto>();

            foreach (ShawahinAPI.Core.Entities.ServicesEntities.Services service in services)
            {
                serviceResponseDtos.Add(new ServiceResponseDto()
                {
                    Id = service.Id,
                    ServiceName = service.ServiceInfo?.ServiceName ?? null,
                    ServiceTypeId = service.ServiceInfo.ServiceType.Id,
                    ServiceTypeName = service.ServiceInfo.ServiceType?.ServiceTypeName ?? null,
                    PhoneNumber = service.ServiceInfo?.PhoneNumber ?? null,
                    Description = service.ServiceInfo?.Description ?? null,
                    UserId = service.UserId,
                    Address = service.ServiceInfo?.Address,
                    City = service.ServiceInfo?.City,
                    ImageUrl = service.ServiceInfo?.ImageUrl,
                });
            }

            return serviceResponseDtos;
        }


        public async static Task<ServiceResponseDto> ToResponse(
         ShawahinAPI.Core.Entities.ServicesEntities.Services service ,
         IRepository<ServiceInfo> repositoryInfo,
         IRepository<ServiceType> repositoryType)
        {
            var serviceResponseDtos = new ServiceResponseDto();

            var serviceInfo = await repositoryInfo.GetByIdAsync(service.ServiceInfoId);
            var serviceType = await repositoryType.GetByIdAsync(serviceInfo?.ServiceTypeId ?? Guid.Empty);

            return new ServiceResponseDto()
            {
                Id = service.Id,
                ServiceName = serviceInfo?.ServiceName ?? null,
                ServiceTypeId = serviceType?.Id ?? Guid.Empty,
                ServiceTypeName = serviceType?.ServiceTypeName ?? null,
                PhoneNumber = serviceInfo?.PhoneNumber ?? null,
                Description = serviceInfo?.Description ?? null,
                UserId = service.UserId,
                Address = serviceInfo?.Address,
                City = serviceInfo?.City,
                ImageUrl = serviceInfo?.ImageUrl,
            };
        }

    }
}