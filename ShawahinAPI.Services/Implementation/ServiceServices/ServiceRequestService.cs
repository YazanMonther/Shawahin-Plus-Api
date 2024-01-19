using ShawahinAPI.Core.DTO;
using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.Entities.ServicesEntitiess;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract;
using ShawahinAPI.Services.Contract.IServiceServices;
using ShawahinAPI.Services.Implementation.Helpers;
using static System.Collections.Specialized.BitVector32;

namespace ShawahinAPI.Services.Implementation.ServiceServices
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IRepository<ServiceRequest> _repository;
        private readonly IRepository<ServiceInfo> _serviceInfoRepository;
        private readonly IRepository<Core.Entities.ServicesEntitiess.ServiceType> _serviceTypeRepository;
        private readonly IEmailService _emailService;
        private readonly IUserGetRepository _userGetRepository;

        public ServiceRequestService(IRepository<ServiceRequest> repository ,
          IRepository<ServiceInfo> serviceInfoRepository,
          IRepository<Core.Entities.ServicesEntitiess.ServiceType> serviceTypeRepository
            , IEmailService email, IUserGetRepository userGetRepository)
        {
            _repository = repository;
            _serviceInfoRepository = serviceInfoRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _emailService = email;
            _userGetRepository = userGetRepository;
        }

        public async Task<IEnumerable<ServiceRequestResponseDto?>> GetAllServiceRequestsAsync()
        {
            // Retrieve all service requests
            var entities = await _repository.GetAllAsync();

            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<ServiceRequestResponseDto?>();
            }

            // Convert entities to response DTOs
            return await ServiceDataHelper.ToResponseRequestList(
                entities,
                _serviceInfoRepository,
                _serviceTypeRepository
            );
        }


        public async Task<ServiceRequestResponseDto> GetServiceRequestByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if(entity == null)
            {
                throw new ArgumentNullException("No Request with this service id .");
            }

            return await ServiceDataHelper.ToResponseRequest(entity, _serviceInfoRepository, _serviceTypeRepository);
        }

        public async Task<ResultDto> AddServiceRequestAsync(ServiceAddRequest requestDto )
        {

            var serviceReqBase = ToServiceRequestMapper.toServiceReqeustBase(requestDto);

            var serviceInfoBase = ToServiceRequestMapper.toServiceInfoBase(requestDto);

            var serviceInfo = ToServiceRequestMapper.toServiceInfo(serviceInfoBase);

            var serviceRequest = ToServiceRequestMapper.toServiceRequest(serviceReqBase, serviceInfo);

            serviceRequest.RequestStatus = RequestStatus.Pending.ToString();

            var addInfo = await _serviceInfoRepository.AddAsync(serviceInfo);
            var result = await _repository.AddAsync(serviceRequest);

            if (result.Succeeded)
            {
                var User = await _userGetRepository.GetUserByIdAsync(requestDto.UserId);
                try
                {
                    EmailRequest emailRequestSubmission = new EmailRequest()
                    {
                        ToEmail = User?.Email!,
                        Subject = "Service Request Submission",
                        Body = $"Dear User,\n\n" +
                                $"Thank you for submitting a Service Request.\n" +
                                $"Our team will review the provided information shortly.\n" +
                                $"You will receive an email once a decision has been made regarding your request.\n\n" +
                                $"Best regards,\n" +
                                $"Shawahin Plus"
                    };
                    await _emailService.SendEmailAsync(emailRequestSubmission);

                }
                catch (Exception ex)
                {

                    return new ResultDto { Succeeded = result.Succeeded, Message = $"{result.Message}, Error sending email: {ex.Message}" };
                }
            }

            return result;
        }

        public async Task<ResultDto> RemoveServiceRequestAsync(Guid id)
        {

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                // Handle not found scenario
                return new ResultDto {Succeeded = false, Message = "Service request not found." };
            }

            var result = await _repository.RemoveAsync(entity);
            return result;
        }

        public async Task<ResultDto> DenayServiceRequestAsync(Guid? requestId)
        {
            if (requestId == null)
            {
                return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
            }

            var getServicenReq = await _repository.GetByIdAsync(requestId.Value);

            if (getServicenReq == null)
            {
                return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
            }
            if (getServicenReq.RequestStatus == RequestStatus.Denied.ToString())
            {
                return new ResultDto() { Succeeded = false, Message = "Service Request Already Denied" };
            }
            if (getServicenReq.RequestStatus == RequestStatus.Accepted.ToString() || getServicenReq.RequestStatus == "Processed")
            {
                return new ResultDto() { Succeeded = false, Message = "Cant denay Accepted Request" };
            }

            getServicenReq.RequestStatus = RequestStatus.Denied.ToString();
            var DenayResult = await _repository.UpdateAsync(getServicenReq);

            if (DenayResult.Succeeded)
            {
                    var User = await _userGetRepository.GetUserByIdAsync(getServicenReq.UserId);
                    try
                    {
                    EmailRequest emailRequestDenial = new EmailRequest()
                    {
                        ToEmail = User?.Email!,
                        Subject = "Service Request Denied",
                        Body = $"Dear User,\n\n" +
                                $"We regret to inform you that your Service Request has been denied.\n" +
                                $"If you have further questions or concerns, please contact our support team.\n\n" +
                                $"Best regards,\n" +
                                $"Shawahin Plus"
                    };
                    await _emailService.SendEmailAsync(emailRequestDenial);

                    }
                catch (Exception ex)
                    {

                        return new ResultDto { Succeeded = DenayResult.Succeeded, Message = $"{DenayResult.Message}, Error sending email: {ex.Message}" };
                    }
                
                return new ResultDto() { Succeeded = true, Message = "Service Request Denied " };
            }

            return new ResultDto() { Succeeded = false, Message = "Failed to Denay Request" };

        }

    }

}
