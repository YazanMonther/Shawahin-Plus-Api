using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers.ChargingStationsMapper;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Implementation.Helpers;

namespace ShawahinAPI.Services.Implementation
{
    public class FavoriteStationsService : IFavoriteStationsService
    {
        private readonly IUserFavoriteStationsRepository _userFavoriteStationsRepository;
        private readonly IChargingStationRepository _stationRepository;
        private readonly IUserGetRepository _userRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IStationOpeningHoursRepository _stationOpeningHoursRepository;
        private readonly ILocationsRepository _locationsRepository;
        private readonly IChargersRepository _chargersRepository;
        private readonly IChargerTypeRepository _chargerTypeRepository;

        public FavoriteStationsService(
            IUserFavoriteStationsRepository userFavoriteStationRepository,
            IChargingStationRepository stationRepository,
            IUserGetRepository userRepository,
            IContactRepository contactRepository,
            IStationOpeningHoursRepository stationOpeningHoursRepository,
            ILocationsRepository locationsRepository,
            IChargersRepository chargersRepository,
            IChargerTypeRepository chargerTypeRepository)
        {
            _userFavoriteStationsRepository = userFavoriteStationRepository ?? throw new ArgumentNullException(nameof(userFavoriteStationRepository));
            _stationRepository = stationRepository ?? throw new ArgumentNullException(nameof(stationRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _stationOpeningHoursRepository = stationOpeningHoursRepository ?? throw new ArgumentNullException(nameof(stationOpeningHoursRepository));
            _locationsRepository = locationsRepository ?? throw new ArgumentNullException(nameof(locationsRepository));
            _chargersRepository = chargersRepository ?? throw new ArgumentNullException(nameof(chargersRepository));
            _chargerTypeRepository = chargerTypeRepository ?? throw new ArgumentNullException(nameof(chargerTypeRepository));
        }


        /// <inheritdoc />
        public async Task<ResultDto?> AddStationToFavoritesAsync(Guid? userId, Guid? stationId)
        {
            if (!userId.HasValue || !stationId.HasValue)
            {
                return new ResultDto
                {
                    Message = "Both userId and stationId must have values.",
                    Succeeded = false
                };
            }

            // Retrieve the user and station entities from the repositories
            var user = await _userRepository.GetUserByIdAsync(userId.Value);
            var station = await _stationRepository.GetStationByIdAsync(stationId.Value);

            if (user == null || station == null)
            {
                return new ResultDto
                {
                    Message = "User or station not found.",
                    Succeeded = false
                };
            }

            // Call the repository method to add the station to favorites
            return await _userFavoriteStationsRepository.AddStationToFavoritesAsync(user, station);
        }

        public  async Task<IEnumerable<ChargingStationDto?>> GetFavoriteStationsAsync(Guid? userId)
        {
            // Validate input parameters
            if (userId == null || userId == Guid.Empty)
            {
                throw new ArgumentException("Invalid userId parameter.", nameof(userId));
            }

            var chargingStations = await _userFavoriteStationsRepository.GetFavoriteStationsAsync(userId.Value);

            if (chargingStations == null)
            {
                throw new InvalidOperationException("Failed to retrieve favorite charging stations.");
            }

            var addStationsData = await StationsDataHelper.AddStationsForeignData(chargingStations, _userRepository,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository, _chargerTypeRepository);

            if(addStationsData is null)
            {
                throw new InvalidOperationException("Failed to add favorite charging stations data .");
            }

            var stationsDtoList = ChargingStationsListMapper.MapToChargingStationsListDto(addStationsData);

            return stationsDtoList;
        }

        public Task<bool> IsStationInFavoritesAsync(Guid? userId, Guid? stationId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto?> RemoveStationFromFavoritesAsync(Guid? userId, Guid? stationId)
        {
            throw new NotImplementedException();
        }
    }
}
