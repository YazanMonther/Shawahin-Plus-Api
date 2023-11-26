using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.ChargingStationsRepositories
{
    public class ChargersRepository : IChargersRepository
    {
        private readonly ShawahinDbContext _context;

        public ChargersRepository(ShawahinDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Add

        public async Task<ResultDto?> AddAsync(Chargers? charger)
        {
            if (charger is null) return new ResultDto() { Succeeded = false, Message = "Invalid operation: Charger is null." };

            try
            {
                await _context.Chargers.AddAsync(charger);
                await _context.SaveChangesAsync();
                return new ResultDto() { Succeeded = true, Message = "Charger Added Successfully." };
            }
            catch (Exception e)
            {
                return new ResultDto() { Succeeded = false, Message = $"Invalid operation: {e.Message}" };
            }
        }

        #endregion

        #region Get

        public async Task<IEnumerable<Chargers>?> GetAllAsync()
        {
            return await _context.Chargers.ToListAsync();
        }
        #endregion

        public async Task<Chargers?> GetByIdAsync(Guid? id)
        {
            return await _context.Chargers.FindAsync(id);
        }

        public async Task<IEnumerable<ChargingStations?>?> GetChargingStationsByPower(ChargerPower? power)
        {
            return await _context.Stations.Where(s => s.Chargers != null && s.Chargers.PowerKw == power).ToListAsync();
        }


        #region Remove

        public async Task<ResultDto?> RemoveAsync(Chargers? charger)
        {
            if (charger is null) return new ResultDto() { Succeeded = false, Message = "Invalid operation: Charger is null." };

            try
            {
                _context.Chargers.Remove(charger);
                await _context.SaveChangesAsync();
                return new ResultDto() { Succeeded = true, Message = "Charger Removed Successfully." };
            }
            catch (Exception e)
            {
                return new ResultDto() { Succeeded = false, Message = $"Invalid operation: {e.Message}" };
            }
        }

        #endregion

        #region Update

        public async Task<ResultDto?> UpdateAsync(Chargers? charger)
        {
            if (charger is null) return new ResultDto() { Succeeded = false, Message = "Invalid operation: Charger is null." };

            try
            {
                _context.Chargers.Update(charger);
                await _context.SaveChangesAsync();
                return new ResultDto() { Succeeded = true, Message = "Charger Updated Successfully." };
            }
            catch (Exception e)
            {
                return new ResultDto() { Succeeded = false, Message = $"Invalid operation: {e.Message}" };
            }
        }

        #endregion
    }

}
