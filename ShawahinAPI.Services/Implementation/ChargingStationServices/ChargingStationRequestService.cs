using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers.ChargingStationsReqMapper;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Implementation.Helpers;

public class ChargingStationRequestService : IChargingStationRequestService
{
    private readonly IRepository<ChargingStationRequests> _chargingStationRequestRepository;
    private readonly IRepository<ApplicationUser> _userGetRepository;
    private readonly IRepository<Contacts> _contactRepository;
    private readonly IRepository<StationOpeningHours> _stationOpeningHoursRepository;
    private readonly IRepository<Locations> _locationsRepository;
    private readonly IRepository<Chargers> _chargersRepository;
    private readonly IRepository<ChargerType> _chargerTypeRepository;

    public ChargingStationRequestService(
        IRepository<ChargingStationRequests> chargingStationRequestRepository,
        IRepository<ApplicationUser> userGetRepository,
        IRepository<Contacts> contactGetByIdRepository,
        IRepository<StationOpeningHours> stationOpeningHoursGetRepository,
        IRepository<Locations> locationsByIdRepository,
        IRepository<Chargers> chargersGetByIdRepository,
        IRepository<ChargerType> chargerTypeByIdRepository)
    {
        _chargingStationRequestRepository = chargingStationRequestRepository;
        _userGetRepository = userGetRepository;
        _contactRepository = contactGetByIdRepository;
        _stationOpeningHoursRepository = stationOpeningHoursGetRepository;
        _locationsRepository = locationsByIdRepository;
        _chargersRepository = chargersGetByIdRepository;
        _chargerTypeRepository = chargerTypeByIdRepository;
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

        var stationsReqDto = await _chargingStationRequestRepository.GetAllAsync();

        var stationsReqDtosData = await StationsDataHelper.AddStationsForeignData(stationsReqDto,
            _userGetRepository, _contactRepository, _stationOpeningHoursRepository,
            _locationsRepository, _chargersRepository, _chargerTypeRepository);

        if (stationsReqDtosData is null)
        {
            throw new ArgumentNullException("No data transformed.");
        }

        return ChargingReqListMapper.MapToStationsDto(stationsReqDtosData);
    }
    #endregion

    #region GetChargingStationRequestByIdAsync Implementation

    public async Task<ChargingStationsReqResponseDto?> GetChargingStationRequestByIdAsync(Guid? requestId)
    {
        if (requestId == null)
            return null;
        var getStation = await _chargingStationRequestRepository.GetByIdAsync(requestId.Value);

        if (getStation == null)
            return null;

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

    #endregion
}
