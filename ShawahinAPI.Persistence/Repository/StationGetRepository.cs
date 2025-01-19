using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.DTO.UserDTO;

namespace ShawahinAPI.Persistence.Repository.ChargingStationsRepositories
{
    public class StationGetRepository : IStationGetRepository
    {
        private readonly ShawahinDbContext _context;

        public StationGetRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        #region IChargingStationRepository Implementation

        public async Task<IEnumerable<ChargingStations?>> GetChargerStationByChargerTypeAsync(ChargersType? type)
        {
            return await _context.Stations.Where(s => s.Chargers != null &&
            s.Chargers.ChargerType != null &&
            s.Chargers.ChargerType.Charger_Type == type).ToListAsync();
        }

        public async Task<IEnumerable<ChargingStations?>?> GetChargingStationsByPower(ChargerPower? power)
        {
            return await _context.Stations.Where(s => s.Chargers != null && s.Chargers.PowerKw == power).ToListAsync();
        }

        public async Task<IEnumerable<ChargingStations?>> GetStationsByPaymentMethodAsync(PaymentMethod? paymentMethod)
        {
            return await _context.Stations.Where(s => s.Chargers != null &&
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
                .Where(station => station.UserId == userId && !station.IsDeleted) // Ensures soft delete is respected
                .ToListAsync();

            return stationsByUser;
        }

        public async Task<ResultDto> AddAsync(ChargingStations station)
        {
            try
            {
                if (station is BaseEntity baseEntity)
                {
                    baseEntity.CreatedDate = DateTime.UtcNow; // Set CreatedDate
                }

                await _context.Stations.AddAsync(station);
                await _context.SaveChangesAsync();
                return new ResultDto { Succeeded = true, Message = "Charging station added successfully." };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = $"Error adding charging station: {ex.Message}" };
            }
        }

        public async Task<ResultDto> UpdateAsync(ChargingStations station)
        {
            try
            {
                if (station is BaseEntity baseEntity)
                {
                    baseEntity.UpdatedDate = DateTime.UtcNow; // Set UpdatedDate
                }

                _context.Entry(station).State = EntityState.Modified;
                _context.Stations.Update(station);
                await _context.SaveChangesAsync();
                return new ResultDto { Succeeded = true, Message = "Charging station updated successfully." };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = $"Error updating charging station: {ex.Message}" };
            }
        }

        public async Task<ResultDto> RemoveAsync(ChargingStations station)
        {
            try
            {
                if (station is BaseEntity baseEntity)
                {
                    baseEntity.IsDeleted = true; // Set IsDeleted for soft delete
                    await UpdateAsync(station); // Update the entity to mark it as deleted
                }

                return new ResultDto { Succeeded = true, Message = "Charging station removed successfully." };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = $"Error removing charging station: {ex.Message}" };
            }
        }

        #endregion
    }
}
