using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Persistence.Repository.ChargingStationsRepositories
{
    public class ChargingStationRepository : IChargingStationRepository
    {
        private readonly ShawahinDbContext _context;

        public ChargingStationRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        #region IChargingStationRepository Implementation

        #region AddStationAsync

        public async Task<ResultDto?> AddStationAsync(ChargingStations? station)
        {
            if (station == null)
            {
                return new ResultDto()
                {
                    Message = "Invalid station parameter",
                    Succeeded = false
                };
            }

            try
            {
                _context.Stations.Add(station);
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    Succeeded = true,
                    Message = "Station added successfully"
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = $"Error adding station: {ex.Message}"
                };
            }
        }

        #endregion

        #region GetStationsAddedByUserAsync

        public async Task<IEnumerable<ChargingStations?>> GetStationsAddedByUserAsync(Guid? userId)
        {
            try
            {
                return await _context.Stations.Where(s => s.UserId == userId).ToListAsync();
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return new List<ChargingStations?>();
            }
        }

        #endregion

        #region GetAllStationsAsync

        public async Task<IEnumerable<ChargingStations?>> GetAllStationsAsync()
        {
            try
            {
                return await _context.Stations.ToListAsync();
            }
            catch (Exception)
            {
                return new List<ChargingStations?>();
            }
        }

        #endregion



        #region GetStationByIdAsync

        public async Task<ChargingStations?> GetStationByIdAsync(Guid? stationId)
        {
            return await _context.Stations.FindAsync(stationId);
        }

        #endregion

        #region RemoveStationAsync

        public async Task<ResultDto> RemoveStationAsync(ChargingStations? station)
        {
            if (station == null)
            {
                return new ResultDto()
                {
                    Message = "Invalid station parameter",
                    Succeeded = false
                };
            }

            try
            {
                var result = _context.Stations.Remove(station);
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    Succeeded = true,
                    Message = "Station removed successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = $"Error removing station: {ex.Message}"
                };
            }
        }

        #endregion

        #region UpdateStationAsync

        public async Task<ResultDto?> UpdateStationAsync(ChargingStations? station)
        {
            if (station == null)
            {
                return new ResultDto()
                {
                    Message = "Invalid station parameter",
                    Succeeded = false
                };
            }

            try
            {
                var result = _context.Stations.Update(station);
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    Succeeded = true,
                    Message = "Station updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = $"Error updating station: {ex.Message}"
                };
            }
        }

        public async Task<IEnumerable<ChargingStations?>> GetStationsByPaymentMethodAsync(PaymentMethod? paymentMethod)
        {
        
            return await _context.Stations.Where(s => s.Chargers!=null &&
            s.Chargers.PaymentMethod == paymentMethod).ToListAsync();
        }

        public async Task<IEnumerable<ChargingStations?>> GetStationsByPaymentTypeAsync(PaymentType? paymentType)
        {
            return await _context.Stations.Where(s => s.Chargers != null
            && s.Chargers.PaymentType == paymentType).ToListAsync();
        }

        public async Task<IEnumerable<ChargingStations?>> GetStationsByChargerStatusAsync(CurrentChargerStatus? chargerStatus)
        {
            return await _context.Stations.Where(s => s.Chargers != null &&
            s.Chargers.CurrentChargerStatus == chargerStatus).ToListAsync();
        }

        public async Task<IEnumerable<ChargingStations?>> GetStationsByUserIdAsync(Guid? userId)
        {
            if (userId == null)
            {
                return Enumerable.Empty<ChargingStations?>();
            }

            var stationsByUser = await _context.Stations
                .Where(station => station.UserId == userId) 
                .ToListAsync();

            return stationsByUser;
        }



        #endregion

        #endregion
    }
}
 