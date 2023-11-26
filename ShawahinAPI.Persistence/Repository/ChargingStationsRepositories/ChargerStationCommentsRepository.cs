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
    public class ChargerStationCommentsRepository : IChargerStationCommentsRepository
    {
        private readonly ShawahinDbContext _context;

        public ChargerStationCommentsRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        #region Add

        public async Task<ResultDto> AddCommentAsync(ChargerStationComments comment)
        {
            try
            {
                if (comment == null)
                {
                    return new ResultDto { Succeeded = false, Message = "Invalid comment data." };
                }

                _context.StationComments.Add(comment);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = ex.Message };
            }
        }
        #endregion
        #region Get
        public async Task<IEnumerable<ChargerStationComments>> GetCommentsForStationAsync(Guid stationId)
        {
            return await _context.StationComments
                .Where(comment => comment.StationId == stationId)
                .ToListAsync();
        }
        #endregion

        #region Remove
        public async Task<ResultDto> RemoveCommentAsync(ChargerStationComments comment)
        {
            try
            {
                if (comment == null)
                {
                    return new ResultDto { Succeeded = false, Message = "Invalid comment data." };
                }

                _context.StationComments.Remove(comment);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = ex.Message };
            }
        }
        #endregion

        #region Update
        public async Task<ResultDto> UpdateCommentAsync(ChargerStationComments comment)
        {
            try
            {
                if (comment == null)
                {
                    return new ResultDto { Succeeded = false, Message = "Invalid comment data." };
                }

                _context.StationComments.Update(comment);
                await _context.SaveChangesAsync();

                return new ResultDto { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = ex.Message };
            }
        }
        #endregion

        #region GetById

        public async Task<ChargerStationComments?> GetCommentByIdAsync(Guid commentId)
        {
            return await _context.StationComments.FindAsync(commentId);
        }

        #endregion
    }
}
