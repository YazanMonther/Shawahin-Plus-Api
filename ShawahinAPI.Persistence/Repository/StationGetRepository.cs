using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.Enums;

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
            s.Chargers.ChargerType.Charger_Type == type).ToListAsync(); ;
        }
        public async Task<IEnumerable<ChargingStations?>?> GetChargingStationsByPower(ChargerPower? power)
        {
            return await _context.Stations.Where(s => s.Chargers != null && s.Chargers.PowerKw == power).ToListAsync();
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

        
    }
}
 