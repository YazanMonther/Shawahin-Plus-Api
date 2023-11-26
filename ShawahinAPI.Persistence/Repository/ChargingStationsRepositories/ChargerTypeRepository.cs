using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;


namespace ShawahinAPI.Persistence.Repository.ChargingStationsRepositories
{
    public class ChargerTypeRepository : IChargerTypeRepository
    {
        private readonly ShawahinDbContext _context;

        public ChargerTypeRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        #region IChargerTypeRepository Implementation

        #region AddChargerTypeAsync

        public async Task<ResultDto?> AddChargerTypeAsync(ChargerType? chargerType)
        {
            try
            {
                if (chargerType == null)
                {
                    return new ResultDto { Succeeded = false, Message = "Invalid charger type data." };
                }

                _context.ChargerTypes.Add(chargerType);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = ex.Message };
            }
        }

        #endregion

        #region GetAllChargerTypesAsync

        public async Task<IEnumerable<ChargerType?>> GetAllChargerTypesAsync()
        {
            return await _context.ChargerTypes.ToListAsync();
        }

        public async Task<IEnumerable<ChargingStations?>> GetChargerStationByChargerTypeAsync(ChargersType? type)
        {
            return await _context.Stations.Where(s =>  s.Chargers != null && 
            s.Chargers.ChargerType != null &&
            s.Chargers.ChargerType.Charger_Type == type).ToListAsync(); ;
        }

        #endregion

        #region GetChargerTypeByIdAsync

        public async Task<ChargerType?> GetChargerTypeByIdAsync(Guid? chargerTypeId)
        {
            return await _context.ChargerTypes.FindAsync(chargerTypeId);
        }

        #endregion

        #region RemoveChargerTypeAsync

        public async Task<ResultDto?> RemoveChargerTypeAsync(ChargerType chargerType)
        {
            try
            {
                if (chargerType == null)
                {
                    return new ResultDto { Succeeded = false, Message = "Invalid charger type data." };
                }

                _context.ChargerTypes.Remove(chargerType);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = ex.Message };
            }
        }

        #endregion

        #region UpdateChargerTypeAsync

        public async Task<ResultDto?> UpdateChargerTypeAsync(ChargerType chargerType)
        {
            try
            {
                if (chargerType == null)
                {
                    return new ResultDto { Succeeded = false, Message = "Invalid charger type data." };
                }

                _context.ChargerTypes.Update(chargerType);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = ex.Message };
            }
        }

        #endregion

        #endregion
    }
}
