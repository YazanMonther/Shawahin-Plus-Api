using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.ChargingStationsRepositories
{
    public class StationOpeningHoursRepository : IStationOpeningHoursRepository
    {
        private readonly ShawahinDbContext _context;

        public StationOpeningHoursRepository(ShawahinDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region IStationOpeningHoursRepository Implementation

        #region AddAsync

        public async Task<ResultDto?> AddAsync(StationOpeningHours stationOpeningHours)
        {
            try
            {
                await _context.chargingStationsHours.AddAsync(stationOpeningHours);
                await _context.SaveChangesAsync();
                return new ResultDto() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto() { Succeeded = false, Message = ex.Message };
            }
        }

        #endregion

        #region GetAllAsync

        public async Task<IEnumerable<StationOpeningHours?>> GetAllAsync()
        {
            try
            {
                return await _context.chargingStationsHours.ToListAsync();
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return new List<StationOpeningHours?>();
            }
        }

        #endregion

        #region GetByIdAsync

        public async Task<StationOpeningHours?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.chargingStationsHours.FindAsync(id);
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return null;
            }
        }

        #endregion

        #region RemoveAsync

        public async Task<ResultDto?> RemoveAsync(StationOpeningHours stationOpeningHours)
        {
            try
            {
                _context.chargingStationsHours.Remove(stationOpeningHours);
                await _context.SaveChangesAsync();
                return new ResultDto() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto() { Succeeded = false, Message = ex.Message };
            }
        }

        #endregion

        #region UpdateAsync

        public async Task<ResultDto?> UpdateAsync(StationOpeningHours stationOpeningHours)
        {
            try
            {
                _context.chargingStationsHours.Update(stationOpeningHours);
                await _context.SaveChangesAsync();
                return new ResultDto() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto() { Succeeded = false, Message = ex.Message };
            }
        }

        #endregion

        #endregion
    }
}
