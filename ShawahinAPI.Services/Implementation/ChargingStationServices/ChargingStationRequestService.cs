using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers.ChargingStationsReqMapper;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Implementation.Helpers;
using ShawahinAPI.Services.Contract;
using ShawahinAPI.Core.DTO;
using static System.Collections.Specialized.BitVector32;

public class ChargingStationRequestService : IChargingStationRequestService
{
    private readonly IRepository<ChargingStationRequests> _chargingStationRequestRepository;
    private readonly IRepository<ApplicationUser> _userGetRepository;
    private readonly IRepository<Contacts> _contactRepository;
    private readonly IRepository<StationOpeningHours> _stationOpeningHoursRepository;
    private readonly IRepository<Locations> _locationsRepository;
    private readonly IRepository<Chargers> _chargersRepository;
    private readonly IRepository<ChargerType> _chargerTypeRepository;
    private readonly IEmailService _emailService;

    public ChargingStationRequestService(
        IRepository<ChargingStationRequests> chargingStationRequestRepository,
        IRepository<ApplicationUser> userGetRepository,
        IRepository<Contacts> contactGetByIdRepository,
        IRepository<StationOpeningHours> stationOpeningHoursGetRepository,
        IRepository<Locations> locationsByIdRepository,
        IRepository<Chargers> chargersGetByIdRepository,
        IRepository<ChargerType> chargerTypeByIdRepository, IEmailService emailService)
    {
        _chargingStationRequestRepository = chargingStationRequestRepository;
        _userGetRepository = userGetRepository;
        _contactRepository = contactGetByIdRepository;
        _stationOpeningHoursRepository = stationOpeningHoursGetRepository;
        _locationsRepository = locationsByIdRepository;
        _chargersRepository = chargersGetByIdRepository;
        _chargerTypeRepository = chargerTypeByIdRepository;
        _emailService = emailService;
    }

    #region IChargingStationRequestService Implementation

    #region AddChargingStationRequestAsync Implementation

    public async Task<ResultDto> AddChargingStationRequestAsync(AddChargingStationsReqDto? stationDto)
    {
        if (stationDto == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid station request" };
        }

        var userWhichAdded = await _userGetRepository.GetByIdAsync(stationDto.UserId);

        if (userWhichAdded == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid User Id" };
        }

        var stationToAdd = ChargingReqMapper.MapToStationsReq(stationDto, userWhichAdded);

        if (stationToAdd == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid station request" };
        }

        stationToAdd.Request_Status = RequestStatus.Pending;

        var addingStationsRe = await _chargingStationRequestRepository.AddAsync(stationToAdd);

        if (addingStationsRe.Succeeded)
        {
            var User = await _userGetRepository.GetByIdAsync(stationDto.UserId);
            try
            {
                EmailRequest emailRequestSubmission = new EmailRequest()
                {
                    ToEmail = User?.Email!,
                    Subject = "Station Request Submission",
                    Body = $"Dear User,\n\n" +
                            $"Thank you for submitting a Station Adding Request.\n" +
                            $"Our team will review the provided information shortly.\n" +
                            $"You will receive an email once a decision has been made regarding your request.\n\n" +
                            $"Best regards,\n" +
                            $"Shawahin Plus"
                };
                await _emailService.SendEmailAsync(emailRequestSubmission);

            }
            catch (Exception ex)
            {

                return new ResultDto { Succeeded = addingStationsRe.Succeeded, Message = $"{addingStationsRe.Message}, Error sending email: {ex.Message}" };
            }
        }
        return addingStationsRe;
    }
    #endregion

    #region GetAllChargingStationRequestsAsync Implementation

    public async Task<IEnumerable<ChargingStationsReqResponseDto>?> GetAllChargingStationRequestsAsync(Guid? userId)
    {
        var AuthCheck = await UserAuthHelper.ValidateUserAdminId(userId, _userGetRepository);

        if (!AuthCheck.Succeeded)
        {
            throw new UnauthorizedAccessException($"User not authorized: {AuthCheck.Message}");
        }

        var stationsReqDto = await _chargingStationRequestRepository.GetAllAsync(c => c.User, c => c.Contact,
                c => c.StationOpeningHours, c => c.Location, c => c.Chargers, c => c.Chargers.ChargerType);

        //var stationsReqDtosData = await StationsDataHelper.AddStationsForeignData(stationsReqDto,
        //    _userGetRepository, _contactRepository, _stationOpeningHoursRepository,
        //    _locationsRepository, _chargersRepository, _chargerTypeRepository);

        if (stationsReqDto is null)
        {
            throw new ArgumentNullException("No data transformed.");
        }

        return ChargingReqListMapper.MapToStationsDto(stationsReqDto);
    }
    #endregion

    #region GetChargingStationRequestByIdAsync Implementation

    public async Task<ChargingStationsReqResponseDto?> GetChargingStationRequestByIdAsync(Guid? requestId)
    {
        if (requestId == null)
            return null;
        var getStation = await _chargingStationRequestRepository.GetByIdAsync(requestId.Value, c => c.User, c => c.Contact,
                c => c.StationOpeningHours, c => c.Location, c => c.Chargers, c => c.Chargers.ChargerType);
     
        if (getStation == null)
            return null;

        //var stationsReqDtosData = await StationsDataHelper.AddStationForeignData(getStation,
        //    _userGetRepository, _contactRepository, _stationOpeningHoursRepository,
        //    _locationsRepository, _chargersRepository, _chargerTypeRepository);

        if (getStation is null)
        {
            throw new ArgumentNullException("No data transformed.");
        }

        var stationDto = ChargingReqMapper.MapToStationsDto(getStation);


        return stationDto;
    }
    #endregion

    #region UpdateChargingStationRequestStatusAsync Implementation

    public async Task<ResultDto> UpdateChargingStationRequestStatusAsync(Guid? requestId, RequestStatus? status)
    {
        if (requestId == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
        }

        var getStationReq = await _chargingStationRequestRepository.GetByIdAsync(requestId.Value);

        if (getStationReq == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
        }
        getStationReq.Request_Status = status;
        var updateResult = await _chargingStationRequestRepository.UpdateAsync(getStationReq);

        return updateResult;
    }
    #endregion
    
    #region RemoveChargingStationRequestAsync Implementation

    public async Task<ResultDto> RemoveChargingStationRequestAsync(Guid? requestId)
    {
        if (requestId == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
        }

        var getStationReq = await _chargingStationRequestRepository.GetByIdAsync(requestId.Value);

        if (getStationReq == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
        }


        var removeResult = await _chargingStationRequestRepository.RemoveAsync(getStationReq);

        return removeResult;
    }
    #endregion


    public async Task<ResultDto> DenayChargingStationRequestAsync(Guid? requestId)
    {
        if (requestId == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
        }

        var getStationReq = await _chargingStationRequestRepository.GetByIdAsync(requestId.Value);

        if (getStationReq == null)
        {
            return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
        }
        if(getStationReq.Request_Status == RequestStatus.Denied)
        {
            return new ResultDto() { Succeeded = false, Message = "Station Request Already Denied" };
        }
        if (getStationReq.Request_Status == RequestStatus.Accepted)
        {
            return new ResultDto() { Succeeded = false, Message = "cant denay Accepted Request" };
        }

        getStationReq.Request_Status = RequestStatus.Denied;
        var DenayResult = await _chargingStationRequestRepository.UpdateAsync(getStationReq);

        if (DenayResult.Succeeded)
        {
            var User = await _userGetRepository.GetByIdAsync(getStationReq.UserId.Value);
            try
            {
                EmailRequest emailRequestDenied = new EmailRequest()
                {
                    ToEmail = User?.Email!,
                    Subject = "Station Request Denied",
                    Body = $"Dear User,\n\n" +
                            $"We regret to inform you that your Station Adding Request has been denied.\n" +
                            $"Please review the provided information and ensure it meets our requirements.\n" +
                            $"Feel free to contact our support for further assistance.\n\n" +
                            $"Best regards,\n" +
                            $"Shawahin Plus"
                };
                await _emailService.SendEmailAsync(emailRequestDenied);
                return new ResultDto() { Succeeded = true, Message = "Station Request Denied " };

            }
            catch (Exception ex)
            {
              return new ResultDto { Succeeded = DenayResult.Succeeded, Message = $"{DenayResult.Message}, Error sending email: {ex.Message}" };
            }
        }
        return new ResultDto() { Succeeded = false, Message = "Failed to Denay Request" };

    }
    #endregion
}
