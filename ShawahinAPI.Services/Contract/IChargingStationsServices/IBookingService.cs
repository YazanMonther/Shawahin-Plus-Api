using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract.IChargingStationsServices
{
    /// <summary>
    /// Service for managing bookings.
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Makes a booking request.
        /// </summary>
        /// <param name="requestDto">The booking request details.</param>
        /// <returns>A <see cref="ResultDto"/> indicating the result of the operation.</returns>
        Task<ResultDto> CreateBookingRequestAsync(BookingRequestDto requestDto);

        /// <summary>
        /// Accepts a booking request.
        /// </summary>
        /// <param name="bookingId">The ID of the booking to accept.</param>
        ///<param name="UserId">User Id for validation </param>
        /// <returns>A <see cref="ResultDto"/> indicating the result of the operation.</returns>
        Task<ResultDto> AcceptBookingAsync(Guid bookingId,Guid UserId);

        /// <summary>
        /// Denies a booking request.
        /// </summary>
        /// <param name="bookingId">The ID of the booking to deny.</param>
        /// <param name="UserId">User Id for validation </param>
        /// <returns>A <see cref="ResultDto"/> indicating the result of the operation.</returns>
        Task<ResultDto> DenyBookingAsync(Guid bookingId, Guid UserId);

        /// <summary>
        /// Gets bookings associated with a user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>An <see cref="IEnumerable{BookingDto}"/> representing the bookings.</returns>
        Task<IEnumerable<BookingResponseDto>> GetBookingsByUserIdAsync(Guid userId);

        /// <summary>
        /// Gets bookings associated with a station ID.
        /// </summary>
        /// <param name="stationId">The ID of the station.</param>
        /// <returns>An <see cref="IEnumerable{BookingDto}"/> representing the bookings.</returns>
        Task<IEnumerable<BookingResponseDto>> GetBookingsByStationIdAsync(Guid stationId);

        /// <summary>
        /// Gets bookings associated with an owner user ID.
        /// </summary>
        /// <param name="ownerUserId">The ID of the owner user.</param>
        /// <returns>An <see cref="IEnumerable{BookingDto}"/> representing the bookings.</returns>
        Task<IEnumerable<BookingResponseDto>> GetBookingsByOwnerUserIdAsync(Guid ownerUserId);
    }

}
