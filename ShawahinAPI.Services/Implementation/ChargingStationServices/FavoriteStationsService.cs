using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers.ChargingStationsMapper;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Implementation.Helpers;

namespace ShawahinAPI.Core.IRepositories
{
    public class FavoriteStationsService : IFavoriteStationsService
    {
        private readonly IRepository<ApplicationUser> _userGetRepository;
        private readonly IRepository<Contacts> _contactRepository;
        private readonly IRepository<StationOpeningHours> _stationOpeningHoursRepository;
        private readonly IRepository<Locations> _locationsRepository;
        private readonly IRepository<Chargers> _chargersRepository;
        private readonly IRepository<ChargerType> _chargerTypeRepository;
        private readonly IRepository<ChargingStations> _chargingStationRepository;
        private readonly IRepository<FavoriteStations> _userFavoriteStationsRepository;


        public FavoriteStationsService(
            IRepository<FavoriteStations> userFavoriteStationRepository,
            IRepository<ApplicationUser> userRepository,
            IRepository<Contacts> contactRepository,
            IRepository<StationOpeningHours> stationOpeningHoursRepository,
            IRepository<Locations> locationsRepository,
            IRepository<Chargers> chargersRepository,
            IRepository<ChargerType> chargerTypeRepository,
            IRepository<ChargingStations> chargingStationRepository
            )
        {
            _userFavoriteStationsRepository = userFavoriteStationRepository ?? throw new ArgumentNullException(nameof(userFavoriteStationRepository));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _stationOpeningHoursRepository = stationOpeningHoursRepository ?? throw new ArgumentNullException(nameof(stationOpeningHoursRepository));
            _locationsRepository = locationsRepository ?? throw new ArgumentNullException(nameof(locationsRepository));
            _chargersRepository = chargersRepository ?? throw new ArgumentNullException(nameof(chargersRepository));
            _chargerTypeRepository = chargerTypeRepository ?? throw new ArgumentNullException(nameof(chargerTypeRepository));
            _chargingStationRepository = chargingStationRepository;
            _userGetRepository = userRepository;
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

            var isfav = await this.IsStationInFavoritesAsync(userId, stationId);
            if (isfav)
            {
                return new ResultDto
                {
                    Message = "Station is already on the favorite list",
                    Succeeded = false
                };
            }
            var user = await _userGetRepository.GetByIdAsync(userId.Value);
            var station = await _chargingStationRepository.GetByIdAsync(stationId.Value);

            if (user == null || station == null)
            {
                return new ResultDto
                {
                    Message = "User or station not found.",
                    Succeeded = false
                };
            }

            FavoriteStations favorite = new FavoriteStations()
            {
                StationId = station.Id,
                Id = Guid.NewGuid(),
                UserId = user.Id,
                
            };

            var addResult= await _userFavoriteStationsRepository.AddAsync(favorite);

            if (addResult.Succeeded)
            {
                station.FavoriteCount += 1;
              await  _chargingStationRepository.UpdateAsync(station);

            }
            return addResult;
        }

        public async Task<IEnumerable<ChargingStationDto?>> GetFavoriteStationsAsync(Guid? userId)
        {
            // Validate input parameters
            if (userId == null || userId == Guid.Empty)
            {
                throw new ArgumentException("Invalid userId parameter.", nameof(userId));
            }

            var chargingStations = await _userFavoriteStationsRepository.GetByConditionAsync(
                u => u.UserId == userId.Value);

            if (chargingStations == null)
            {
                throw new InvalidOperationException("Failed to retrieve favorite charging stations.");
            }

            var stations = new List<ChargingStations>(); // Replace YourStationType with the actual type of charging stations

            foreach (var chargingStation in chargingStations)
            {
                if (chargingStation != null)
                {
                    var station = await _chargingStationRepository.GetByIdAsync(chargingStation.StationId);
                    stations.Add(station !);
                }
            }

            var addStationsData = await StationsDataHelper.AddStationsForeignData(stations, _userGetRepository,
                _contactRepository, _stationOpeningHoursRepository,
                _locationsRepository, _chargersRepository, _chargerTypeRepository);

            if (addStationsData is null)
            {
                throw new InvalidOperationException("Failed to add favorite charging stations data .");
            }

            var stationsDtoList = StationsMapper.MapToChargingStationsListDto(addStationsData);

            return stationsDtoList;
        }

            public async Task<bool> IsStationInFavoritesAsync(Guid? userId, Guid? stationId)
            {
                if (!userId.HasValue || !stationId.HasValue)
                {
                    throw new ArgumentException("Both userId and stationId must have values.");
                }

                var isStationInFavorites = await _userFavoriteStationsRepository.GetByConditionAsync(
                    f => f.UserId == userId.Value && f.StationId == stationId.Value);
                

                return isStationInFavorites.Any();
            }

            public async Task<ResultDto?> RemoveStationFromFavoritesAsync(Guid? userId, Guid? stationId)
            {
                if (!userId.HasValue || !stationId.HasValue)
                {
                    return new ResultDto
                    {
                        Message = "Both userId and stationId must have values.",
                        Succeeded = false
                    };
                }

                var favoriteStations = await _userFavoriteStationsRepository.GetByConditionAsync(
                    f => f.UserId == userId.Value && f.StationId == stationId.Value);

                if (favoriteStations == null)
                {
                    return new ResultDto
                    {
                        Message = "Favorite stations not found.",
                        Succeeded = false
                    };
                }

            var favoriteStation = favoriteStations.FirstOrDefault();

                if (favoriteStation == null)
                {
                    return new ResultDto
                    {
                        Message = "Favorite station not found.",
                        Succeeded = false
                    };
                }

                var removeResult = await _userFavoriteStationsRepository.RemoveAsync(favoriteStation);

                if (removeResult.Succeeded)
                {
                    var station = await _chargingStationRepository.GetByIdAsync(stationId.Value);
                    if (station != null && station.FavoriteCount > 0)
                    {
                        station.FavoriteCount -= 1;
                        await _chargingStationRepository.UpdateAsync(station);
                    }
                }

                return removeResult;
            }

    }
}
