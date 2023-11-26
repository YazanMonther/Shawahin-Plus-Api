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
    public class ChargingStationRequestRepository : IChargingStationRequestRepository
    {
        private readonly ShawahinDbContext _context;

        public ChargingStationRequestRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        #region IChargingStationRequestRepository Implementation

        #region AddRequestAsync

        public async Task<ResultDto> AddRequestAsync(ChargingStationRequests request)
        {
            if (request == null)
            {
                return new ResultDto()
                {
                    Message = "Invalid request parameter",
                    Succeeded = false
                };
            }

            try
            {
                _context.StationRequest.Add(request);
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    Succeeded = true,
                    Message = "Charging station request added successfully"
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = $"Error adding charging station request: {ex.Message}"
                };
            }
        }

        #endregion

        #region GetAllRequestsAsync

        public async Task<IEnumerable<ChargingStationRequests>> GetAllRequestsAsync()
        {
            try
            {
                return await _context.StationRequest.ToListAsync();
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return new List<ChargingStationRequests>();
            }
        }

        #endregion

        #region GetRequestByIdAsync

        public async Task<ChargingStationRequests?> GetRequestByIdAsync(Guid requestId)
        {
            return await _context.StationRequest.FindAsync(requestId);
        }

        #endregion

        #region UpdateRequestStatusAsync

        public async Task<ResultDto?> UpdateRequestStatusAsync(Guid? requestId, RequestStatus? status)
        {
            try
            {
                // Check for null parameters
                if (requestId == null || status == null)
                {
                    return new ResultDto
                    {
                        Succeeded = false,
                        Message = "Invalid parameters. Both requestId and status must have values.",
                    };
                }

                var request = await _context.StationRequest.FindAsync(requestId);
                if (request == null)
                {
                    return new ResultDto
                    {
                        Succeeded = false,
                        Message = "Request not found.",
                    };
                }

                request.Request_Status = status;
                await _context.SaveChangesAsync();

                // Return a successful result
                return new ResultDto
                {
                    Succeeded = true,
                    Message = "Request status updated successfully.",
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error updating request status: {ex.Message}");

                // Return a failure result
                return new ResultDto
                {
                    Succeeded = false,
                    Message = "An error occurred while updating request status.",
                };
            }
        }

        #endregion

        #region RemoveRequestAsync

        public async Task<ResultDto?> RemoveRequestAsync(ChargingStationRequests? request)
        {
            if (request == null)
            {
                return new ResultDto
                {
                    Succeeded = false,
                    Message = "Invalid request parameter",
                };
            }

            try
            {
                var result = _context.StationRequest.Remove(request);
                await _context.SaveChangesAsync();

                return new ResultDto
                {
                    Succeeded = true,
                    Message = "Charging station request removed successfully.",
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto
                {
                    Succeeded = false,
                    Message = $"Error removing charging station request: {ex.Message}",
                };
            }
        }

        #endregion

        #endregion
    }
}
