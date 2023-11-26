using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Persistence.Repository.ChargingStationsRepositories
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly ShawahinDbContext _context;

        public LocationsRepository(ShawahinDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region ILocationsRepository Implementation

        #region AddAsync

        public async Task<ResultDto?> AddAsync(Locations? location)
        {
            if (location is null) return new ResultDto { Succeeded = false, Message = "Error adding location: Null Location" };

            try
            {
                var result = await _context.Locations.AddAsync(location);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true, Message = "Location added successfully." };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto { Succeeded = false, Message = $"Error adding location: {ex.Message}" };
            }
        }

        #endregion

        #region GetAllAsync

        public async Task<IEnumerable<Locations?>> GetAllAsync()
        {
            try
            {
                return await _context.Locations.ToListAsync();
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return new List<Locations?>();
            }
        }

        #endregion

        #region GetByIdAsync

        public async Task<Locations?> GetByIdAsync(Guid? id)
        {
            try
            {
                return await _context.Locations.FindAsync(id);
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return null;
            }
        }

        #endregion

        #region RemoveAsync

        public async Task<ResultDto?> RemoveAsync(Locations? location)
        {
            if (location is null) return new ResultDto { Succeeded = false, Message = "Error Removing location: Null Location" };

            try
            {
                var result = _context.Locations.Remove(location);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true, Message = "Location Removing successfully." };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto { Succeeded = false, Message = $"Error Removing location: {ex.Message}" };
            }
        }

        #endregion

        #region UpdateAsync

        public async Task<ResultDto?> UpdateAsync(Locations? location)
        {
            if (location is null) return new ResultDto { Succeeded = false, Message = "Error Updating location: Null Location" };

            try
            {
                var result = _context.Locations.Update(location);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true, Message = "Location Updating successfully." };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto { Succeeded = false, Message = $"Error Updating location: {ex.Message}" };
            }
        }

        #endregion

        #endregion
    }
}
