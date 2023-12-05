using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Core.Mappers.ChargingStationsMapper;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Implementation.Helpers;

namespace ShawahinAPI.Services.Implementation
{
    /// <summary>
    /// Service for managing and retrieving charging stations.
    /// </summary>
    public class ChargingStationsService : IChargingStationsService
    {
        private readonly IRepository<ChargingStationRequests> _chargingStationRequestRepository;
        private readonly IRepository<ApplicationUser> _userGetRepository;
        private readonly IRepository<Contacts> _contactRepository;
        private readonly IRepository<StationOpeningHours> _stationOpeningHoursRepository;
        private readonly IRepository<Locations> _locationsRepository;
        private readonly IRepository<Chargers> _chargersRepository;
        private readonly IRepository<ChargerType> _chargerTypeRepository;
        private readonly IRepository<ChargingStations> _chargingStationRepository;
        private readonly IStationGetRepository _stationGetRepository;



        public ChargingStationsService(IRepository<ChargingStationRequests> chargingStationRequestRepository,
        IRepository<ApplicationUser> userGetRepository,
        IRepository<Contacts> contactGetByIdRepository,
        IRepository<StationOpeningHours> stationOpeningHoursGetRepository,
        IRepository<Locations> locationsByIdRepository,
        IRepository<Chargers> chargersGetByIdRepository,
        IRepository<ChargerType> chargerTypeByIdRepository,
        IRepository<ChargingStations> chargerTypeRepository,
        IStationGetRepository station)
        {
            _chargingStationRepository = chargerTypeRepository;
            _chargingStationRequestRepository = chargingStationRequestRepository;
            _userGetRepository = userGetRepository;
            _contactRepository = contactGetByIdRepository;
            _stationOpeningHoursRepository = stationOpeningHoursGetRepository;
            _locationsRepository = locationsByIdRepository;
            _chargersRepository = chargersGetByIdRepository;
            _chargerTypeRepository = chargerTypeByIdRepository;
            _stationGetRepository = station;
        }

        #region IChargingStationsService Implementation

        public async Task<ResultDto> AddNewChargingStationAsync(Guid? requestId, Guid? userId)
        {
            var userAuth = await UserAuthHelper.ValidateUserAdminId(userId, _userGetRepository);

            if (!userAuth.Succeeded)
            {
                return userAuth;
            }
            if (requestId == null || requestId == Guid.Empty) // Check for Guid.Empty
            {
                return new ResultDto() { Succeeded = false, Message = "Empty Request Id" };
            }

            var chargerRequest = await _chargingStationRequestRepository.GetByIdAsync(requestId.Value);

            if (chargerRequest == null)
            {
                return new ResultDto() { Succeeded = false, Message = "Invalid Request Id" };
            }

            var chargingStations = await StationsMapper.MapToChargingStationsAsync(chargerRequest,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository);
           

            var addingResult = await _chargingStationRepository.AddAsync(chargingStations);

            var getStationReq = await _chargingStationRequestRepository.GetByIdAsync(chargerRequest.Id);

            if (getStationReq == null)
            {
                return new ResultDto() { Succeeded = false, Message = "Invalid request Id " };
            }

            getStationReq.Request_Status = RequestStatus.Accepted;
            var statusUpdate = await _chargingStationRequestRepository.UpdateAsync(getStationReq);

            return addingResult;
        }

        public async Task<IEnumerable<ChargingStationDto?>> GetAllChargingStationsAsync()
        {
            var chargingStations = await _chargingStationRepository.GetAllAsync();

            var addStationsData = await StationsDataHelper.AddStationsForeignData(chargingStations, _userGetRepository,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository, _chargerTypeRepository);

            if(addStationsData is null)
            {
                throw new ArgumentNullException("No charging stations Retrived .");
            }

            var stationsDtoList = StationsMapper.MapToChargingStationsListDto(addStationsData);

            return stationsDtoList;
        }

        public async Task<ChargingStationDto?> GetChargingStationByIdAsync(Guid? stationId)
        {
            if (stationId == null)
                return null;

            var station = await _chargingStationRepository.GetByIdAsync(stationId.Value);

            if (station != null)
            {
                var stationDto =  StationsMapper.MapToChargingStationsDto(station);

                return stationDto;
            }
            return null;
        }

        public async Task<ResultDto> UpdateChargingStationAsync(Guid? stationId, ChargingStationDto updatedStation)
        {
            if(stationId == null)
                return new ResultDto() { Succeeded = false, Message = "Invalid stationId Id " };

            var existingStation = await _chargingStationRepository.GetByIdAsync(stationId.Value);

            if (existingStation != null)
            {
                   
                var updateResult = await _chargingStationRepository.UpdateAsync(existingStation);

                return updateResult;
            }
            return new ResultDto { Succeeded = false, Message = "Error updating charging station." };

        }

        public async Task<ResultDto> RemoveChargingStationAsync(Guid? stationId)
        {
            if (stationId == null)
                return new ResultDto() { Succeeded = false, Message = "Invalid stationId Id " };

            var stationToRemove = await _chargingStationRepository.GetByIdAsync(stationId.Value);

            if (stationToRemove != null)
            {
                var removeResult = await _chargingStationRepository.RemoveAsync(stationToRemove);

                    return removeResult;
            }

            return new ResultDto { Succeeded = false, Message = "Error removing charging station." };
        }

        public async Task<IEnumerable<ChargingStationDto?>> GetFilteredChargingStations(string? chargerType,
            string? paymentMethod, string? paymentType, 
            string? chargerPower, string? chargerStatus)
        {
            IEnumerable<ChargingStationDto?> Chargingstaions = new List<ChargingStationDto?>();

            if (chargerType != null)
            {
                var type = EnumHelper.ParseEnum<ChargersType>(chargerType);
                var staions = await _stationGetRepository.
                    GetChargerStationByChargerTypeAsync(type);
              
                var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository, _chargerTypeRepository);

                if(addStationsData !=null)
                Chargingstaions = StationsMapper.
                    MapToChargingStationsListDto(addStationsData);
            }

            if (paymentMethod != null)
            {
                var PayMethod = EnumHelper.ParseEnum<PaymentMethod>(paymentMethod);
                if (!Chargingstaions.Any())
                {
                   var staions = await _stationGetRepository.
                        GetStationsByPaymentMethodAsync(PayMethod);
                 
                    var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                    _contactRepository, _stationOpeningHoursRepository,
                    _locationsRepository, _chargersRepository, _chargerTypeRepository);
                        
                    if(addStationsData !=null)
                    Chargingstaions = StationsMapper.
                        MapToChargingStationsListDto(addStationsData);
                }
                else
                {
                    Chargingstaions = Chargingstaions.Where
                        (s =>  s?.PaymentMethod == paymentMethod);
                }
            }
            if (paymentType != null)
            {
                var PayType = EnumHelper.ParseEnum<PaymentType>(paymentType);
                if (!Chargingstaions.Any())
                {
                    var staions = await _stationGetRepository.
                        GetStationsByPaymentTypeAsync(PayType);
                  
                    var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                    _contactRepository, _stationOpeningHoursRepository,
                    _locationsRepository, _chargersRepository, _chargerTypeRepository);

                    if(addStationsData != null)
                    Chargingstaions = StationsMapper.
                        MapToChargingStationsListDto(addStationsData);
                }
                else
                {
                    Chargingstaions = Chargingstaions.Where(s => s?.PaymentType == 
                    paymentType);
                }
            }

            if (chargerPower != null)
            {
                var CharPower = EnumHelper.ParseEnum<ChargerPower>(chargerPower);
                if (!Chargingstaions.Any())
                {
                    var staions = await _stationGetRepository.
                        GetChargingStationsByPower(CharPower);
                    if (staions != null)
                    {
                        var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                        _contactRepository, _stationOpeningHoursRepository,
                        _locationsRepository, _chargersRepository, _chargerTypeRepository);

                         if(addStationsData !=null)
                        Chargingstaions = StationsMapper.
                            MapToChargingStationsListDto(addStationsData);
                    }
                }
                else
                {
                    Chargingstaions = Chargingstaions.Where(s => s?.PowerKw == chargerPower);
                }
            }

            if (chargerStatus != null)
            {
                var CharStatus = EnumHelper.ParseEnum<CurrentChargerStatus>(chargerStatus);
                if (!Chargingstaions.Any())
                {
                    var staions = await _stationGetRepository.
                        GetStationsByChargerStatusAsync(CharStatus);
                    if (staions != null)
                    {
                        Chargingstaions = StationsMapper.
                                MapToChargingStationsListDto(staions);
                        var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                            _contactRepository, _stationOpeningHoursRepository,
                            _locationsRepository, _chargersRepository, _chargerTypeRepository);
                        if(addStationsData != null)
                        Chargingstaions = StationsMapper.
                            MapToChargingStationsListDto(addStationsData);
                    }
                }
                else
                {
                    Chargingstaions = Chargingstaions.Where(s => s?.ChargerStatus == chargerStatus);
                }
            }

            return Chargingstaions ?? throw new NullReferenceException("No stations meet thie cretiera .");
        }

        public async Task<IEnumerable<UserStationsDto?>> GetChargingStationsByUserIdAsync(Guid? userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("Invalid user Id");
            }
            var chargingStations = await _chargingStationRepository.GetByConditionAsync(s => s.UserId ==userId);

            var addStationsData = await StationsDataHelper.AddStationsForeignData(chargingStations, _userGetRepository,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository, _chargerTypeRepository);

            if (addStationsData is null)
            {
                throw new ArgumentNullException("No charging stations Retrived .");
            }

            var stationsDtoList = UserChargingStationsListMapper.MapToChargingStationsListDto(addStationsData);

            return stationsDtoList;
        }

        #endregion
    }
}
