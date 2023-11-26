using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;

namespace ShawahinAPI.Persistence.Repository.ChargingStationsRepositories
{
    public class UserFavoriteStationsRepository : IUserFavoriteStationsRepository
    {
        private readonly ShawahinDbContext _dbContext;

        public UserFavoriteStationsRepository(ShawahinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region IUserFavoriteStationsRepository Implementation

        #region AddStationToFavoritesAsync

        public async Task<ResultDto?> AddStationToFavoritesAsync(ApplicationUser? user, ChargingStations? station)
        {
            try
            {
                if (user == null || station == null )
                {
                    return new ResultDto() { Message = "Station or user or both are null.", Succeeded = false };
                }

                // Check if the station is not already in the user's favorites
                var isFavorite = await _dbContext.FavoriteStations
                    .AnyAsync(fs => fs.UserId == user.Id && fs.StationId == station.Id);

                if (isFavorite)
                {
                    return new ResultDto() { Message = "Station is already in the user's favorites", Succeeded = false };
                }

                var favoriteStation = new FavoriteStations
                {
                    UserId = user.Id,
                    StationId = station.Id,
                    Station = station,
                    User = user
                };

                _dbContext.FavoriteStations.Add(favoriteStation);
                var stationToFav = _dbContext.Stations.FirstOrDefault(s=> s.Id == station.Id);
                if (stationToFav != null)
                {
                    stationToFav.FavoriteCount += 1;
                    _dbContext.Stations.Update(stationToFav);
                }
                await _dbContext.SaveChangesAsync();

                return new ResultDto() { Succeeded = true, Message = "Station added to favorites." };
            }
            catch (Exception ex)
            {
                return new ResultDto() { Succeeded = false, Message = ex.Message };
            }
        }

        #endregion

        #region GetFavoriteStationsAsync

        public async Task<IEnumerable<ChargingStations?>> GetFavoriteStationsAsync(Guid? userId)
        {
            try
            {
                // Get a list of station IDs
                var favoriteStationIds = await _dbContext.FavoriteStations
                    .Where(fs => fs.UserId == userId)
                    .Select(fs => fs.StationId)
                    .ToListAsync();

                // Get the stations by checking if the stations entity contains these IDs
                var favoriteStations = await _dbContext.Stations
                    .Where(s => favoriteStationIds.Contains(s.Id))
                    .ToListAsync();

                return favoriteStations;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception($" Error : {ex}");
            }
        }

        #endregion

        #region RemoveStationFromFavoritesAsync

        public async Task<ResultDto?> RemoveStationFromFavoritesAsync(Guid? userId, Guid? stationId)
        {
            try
            {
                var favoriteStation = await _dbContext.FavoriteStations
                    .FirstOrDefaultAsync(fs => fs.UserId == userId && fs.StationId == stationId);

                if (favoriteStation != null)
                {
                    _dbContext.FavoriteStations.Remove(favoriteStation);
                    await _dbContext.SaveChangesAsync();

                    return new ResultDto() { Succeeded = true, Message = "Station removed from favorites." };
                }

                return new ResultDto() { Succeeded = false, Message = "Couldn't find this station in the favorite stations." };
            }
            catch (Exception ex)
            {
                return new ResultDto() { Succeeded = false, Message = ex.Message };
            }
        }

        #endregion

        #region IsStationInFavoritesAsync

        public async Task<bool> IsStationInFavoritesAsync(Guid? userId, Guid? stationId)
        {
            try
            {
                return await _dbContext.FavoriteStations
                    .AnyAsync(fs => fs.UserId == userId && fs.StationId == stationId);
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return false;
            }
        }

        #endregion

        #endregion
    }
}
