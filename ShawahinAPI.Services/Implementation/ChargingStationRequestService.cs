using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers.ChargingStationsReqMapper;
using ShawahinAPI.Services.Implementation.Helpers;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShawahinAPI.Core.Mappers;

namespace ShawahinAPI.Services.Implementation.ChargingStationsReqService
{
    /// <summary>
    /// Service for managing charging station requests.
    /// </summary>
    public class ChargingStationRequestService : IChargingStationRequestService
    {
        private readonly IChargingStationRequestRepository _chargingStationRequestRepository;
        private readonly IUserGetRepository _userGetRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IStationOpeningHoursRepository _stationOpeningHoursRepository;
        private readonly ILocationsRepository _locationsRepository;
        private readonly IChargersRepository _chargersRepository;
        private readonly IChargerTypeRepository _chargerTypeRepository;

        public ChargingStationRequestService(IChargingStationRequestRepository chargingStationRequestRepository,
            IUserGetRepository userGetRepository, IContactRepository contactGetByIdRepository,
            IStationOpeningHoursRepository stationOpeningHoursGetRepository, ILocationsRepository locationsByIdRepository,
            IChargersRepository chargersGetByIdRepository, IChargerTypeRepository chargerTypeByIdRepository)
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

        public async Task<ResultDto?> AddChargingStationRequestAsync(AddChargingStationsReqDto? stationDto)
        {

            if (stationDto == null)
            {
                return new ResultDto() { Succeeded = false, Message = "Invalid station request" };
            }

            var userWhichAdded = await _userGetRepository.GetUserByIdAsync(stationDto.UserId);

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

            var addingStationsRe = await _chargingStationRequestRepository.AddRequestAsync(stationToAdd);

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

            // Getting the Request from the db 
            var stationsReqDto = await _chargingStationRequestRepository.GetAllRequestsAsync();

            var stationsReqDtosData = await StationsDataHelper.AddStationsForeignData(stationsReqDto,
                _userGetRepository, _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository, _chargerTypeRepository);

            if( stationsReqDtosData is null)
            {
                throw new ArgumentNullException("No data transformed .");
            }
            return ChargingReqListMapper.MapToStationsDto(stationsReqDtosData);

        }
        #endregion

        #region GetChargingStationRequestByIdAsync Implementation

        public  Task<ChargingStationsReqResponseDto?> GetChargingStationRequestByIdAsync(Guid? requestId)
        {

            throw new NotImplementedException();

        }
        #endregion

       #region UpdateChargingStationRequestStatusAsync Implementation

        public async Task<ResultDto?> UpdateChargingStationRequestStatusAsync(Guid? requestId, RequestStatus? status)
        {

            // Implement the logic to update charging station request status
            // Use _chargingStationRequestRepository.UpdateRequestStatusAsync method

            // Example:
            var updateResult = await _chargingStationRequestRepository.UpdateRequestStatusAsync(requestId, status);

            return updateResult;

        }
        #endregion

       #region RemoveChargingStationRequestAsync Implementation

        public  Task<ResultDto?> RemoveChargingStationRequestAsync(Guid? requestId)
        {

            // Implement the logic to remove charging station request
            // Use _chargingStationRequestRepository.RemoveRequestAsync method
            throw new NotImplementedException();
            // Example:
           // var removeResult = await _chargingStationRequestRepository.RemoveRequestAsync(requestId.Value);

         //   return removeResult;

          
        }
        #endregion

        #endregion
    }
}
