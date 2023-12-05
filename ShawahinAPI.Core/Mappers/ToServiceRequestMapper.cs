using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.Entities.ServicesEntities;

namespace ShawahinAPI.Core.Mappers
{
    public static class ToServiceRequestMapper
    {

        public static ServiceRequestBaseDto toServiceReqeustBase(ServiceAddRequest serviceAddRequest)
        {
            return new ServiceRequestBaseDto()
            {
                ServiceTypeId = serviceAddRequest.ServiceTypeId,
                UserId = serviceAddRequest.UserId
            };
        }

        public static ServiceInfoBaseDto toServiceInfoBase(ServiceAddRequest serviceAddRequest)
        {
            return new ServiceInfoBaseDto()
            {
                ServiceTypeId = serviceAddRequest.ServiceTypeId,
                Description = serviceAddRequest.Description,
                ServiceName = serviceAddRequest.ServiceName,
                Address = serviceAddRequest.Address,
                City = serviceAddRequest.City,
                ImageUrl = serviceAddRequest.ImageUrl,
                PhoneNumber = serviceAddRequest.PhoneNumber
            };
        }

        public static ServiceRequest toServiceRequest(ServiceRequestBaseDto serviceRequest , ServiceInfo serviceInfo)
        {
            return new ServiceRequest()
            {
                Id = Guid.NewGuid(),
                UserId = serviceRequest.UserId,
                ServiceInfo = serviceInfo,
                ServiceInfoId = serviceInfo.Id,
                
            };
        }

        public static ServiceInfo toServiceInfo(ServiceInfoBaseDto infoBase)
        {
            return new ServiceInfo()
            {
                Id = Guid.NewGuid(),
                ServiceName = infoBase.ServiceName,
                ServiceTypeId = infoBase.ServiceTypeId,
                PhoneNumber = infoBase.PhoneNumber,
                Description = infoBase.Description,
                Address =  infoBase.Address,
                City = infoBase.City,
                ImageUrl = infoBase.ImageUrl
            };
        }
    }
}
