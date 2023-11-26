using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Core.Mappers.ChargingStationsMapper;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Implementation.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Implementation
{
    /// <summary>
    /// Service for managing and retrieving charging stations.
    /// </summary>
    public class ChargingStationsService : IChargingStationsService
    {
        private readonly IChargingStationRepository _chargingStationRepository;
        private readonly IChargingStationRequestRepository _chargingStationRequestRepository;
        private readonly IUserGetRepository _userGetRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IStationOpeningHoursRepository _stationOpeningHoursRepository;
        private readonly ILocationsRepository _locationsRepository;
        private readonly IChargersRepository _chargersRepository;
        private readonly IChargerTypeRepository _chargerTypeRepository;


        public ChargingStationsService(IUserGetRepository userGet, IChargingStationRepository chargingStationRepository,
            IChargingStationRequestRepository chargingStationRequestRepository,
            IContactRepository contact, IStationOpeningHoursRepository stationOpening,
            ILocationsRepository locationsRepository, IChargersRepository chargers,
             IChargerTypeRepository chargerTypeRepository)
        {
            _chargingStationRepository = chargingStationRepository;
            _chargingStationRequestRepository = chargingStationRequestRepository;
            _userGetRepository = userGet;
            _contactRepository = contact;
            _stationOpeningHoursRepository = stationOpening;
            _locationsRepository = locationsRepository;
            _chargersRepository = chargers;
            _chargerTypeRepository = chargerTypeRepository;
        }

        #region IChargingStationsService Implementation

        public async Task<ResultDto?> AddNewChargingStationAsync(Guid? requestId, Guid? userId)
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

            var chargerRequest = await _chargingStationRequestRepository.GetRequestByIdAsync(requestId.Value);

            if (chargerRequest == null)
            {
                return new ResultDto() { Succeeded = false, Message = "Invalid Request Id" };
            }

            var chargingStations = await ChargingStationsMapper.MapToChargingStationsAsync(chargerRequest,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository);
           

            var addingResult = await _chargingStationRepository.AddStationAsync(chargingStations);

            var statusUpdate = await _chargingStationRequestRepository.UpdateRequestStatusAsync(chargerRequest.Id, RequestStatus.Accepted);

            return addingResult;
        }

        public async Task<IEnumerable<ChargingStationDto?>> GetAllChargingStationsAsync()
        {
            var chargingStations = await _chargingStationRepository.GetAllStationsAsync();

            var addStationsData = await StationsDataHelper.AddStationsForeignData(chargingStations, _userGetRepository,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository, _chargerTypeRepository);

            if(addStationsData is null)
            {
                throw new ArgumentNullException("No charging stations Retrived .");
            }

            var stationsDtoList = ChargingStationsListMapper.MapToChargingStationsListDto(addStationsData);

            return stationsDtoList;
        }

        public async Task<ChargingStationDto?> GetChargingStationByIdAsync(Guid? stationId)
        {
            var station = await _chargingStationRepository.GetStationByIdAsync(stationId);
            if (station != null)
            {
                // Map to ChargingStationDto using a mapper if needed
                return new ChargingStationDto
                {
                    // Map properties from ChargingStations entity to ChargingStationDto
                    // Example: Id = station.Id, Name = station.Name, ...
                };
            }
            return null;
        }

        public async Task<ResultDto?> UpdateChargingStationAsync(Guid? stationId, ChargingStationDto updatedStation)
        {
            var existingStation = await _chargingStationRepository.GetStationByIdAsync(stationId);

            if (existingStation != null)
            {
                // Update existingStation properties with the values from updatedStation
                // Example: existingStation.Name = updatedStation.Name, ...

                // Save changes
                var updateResult = await _chargingStationRepository.UpdateStationAsync(existingStation);

                if ( updateResult is null ||  !updateResult.Succeeded)
                {
                    return new ResultDto { Succeeded = false, Message = "Error updating charging station." };

                }
            }
            return new ResultDto { Succeeded = true, Message = "Charging station updated successfully." };

        }

        public async Task<ResultDto?> RemoveChargingStationAsync(Guid? stationId)
        {
            var stationToRemove = await _chargingStationRepository.GetStationByIdAsync(stationId);

            if (stationToRemove != null)
            {
                var removeResult = await _chargingStationRepository.RemoveStationAsync(stationToRemove);

                if (removeResult.Succeeded)
                {
                    return new ResultDto { Succeeded = true, Message = "Charging station removed successfully." };
                }
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
                var staions = await _chargerTypeRepository.
                    GetChargerStationByChargerTypeAsync(type);
              
                var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository, _chargerTypeRepository);

                if(addStationsData !=null)
                Chargingstaions = ChargingStationsListMapper.
                    MapToChargingStationsListDto(addStationsData);
            }

            if (paymentMethod != null)
            {
                var PayMethod = EnumHelper.ParseEnum<PaymentMethod>(paymentMethod);
                if (!Chargingstaions.Any())
                {
                   var staions = await _chargingStationRepository.
                        GetStationsByPaymentMethodAsync(PayMethod);
                 
                    var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                    _contactRepository, _stationOpeningHoursRepository,
                    _locationsRepository, _chargersRepository, _chargerTypeRepository);
                        
                    if(addStationsData !=null)
                    Chargingstaions = ChargingStationsListMapper.
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
                    var staions = await _chargingStationRepository.
                        GetStationsByPaymentTypeAsync(PayType);
                  
                    var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                    _contactRepository, _stationOpeningHoursRepository,
                    _locationsRepository, _chargersRepository, _chargerTypeRepository);

                    if(addStationsData != null)
                    Chargingstaions = ChargingStationsListMapper.
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
                    var staions = await _chargersRepository.
                        GetChargingStationsByPower(CharPower);
                    if (staions != null)
                    {
                        var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                        _contactRepository, _stationOpeningHoursRepository,
                        _locationsRepository, _chargersRepository, _chargerTypeRepository);

                         if(addStationsData !=null)
                        Chargingstaions = ChargingStationsListMapper.
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
                    var staions = await _chargingStationRepository.
                        GetStationsByChargerStatusAsync(CharStatus);
                    if (staions != null)
                    {
                        Chargingstaions = ChargingStationsListMapper.
                                MapToChargingStationsListDto(staions);
                        var addStationsData = await StationsDataHelper.AddStationsForeignData(staions, _userGetRepository,
                            _contactRepository, _stationOpeningHoursRepository,
                            _locationsRepository, _chargersRepository, _chargerTypeRepository);
                        if(addStationsData != null)
                        Chargingstaions = ChargingStationsListMapper.
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
            var chargingStations = await _chargingStationRepository.GetStationsAddedByUserAsync(userId);

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
