using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Core.Mappers.ChargingStationsMapper
{
    public static class ChargerBookingsMapper
    {
        // Helper method to map DTO to Entity
        public static Bookings MapDtoToEntity(BookingRequestDto requestDto)
        {
            var bookingEntity = new Bookings
            {
                StationId = requestDto.StationId,
                UserId = requestDto.UserId,
                Status = BookingStatus.Pending.ToString(),
                startBooking = requestDto.StartBooking,
                endBooking = requestDto.EndBooking,
            };
            return bookingEntity;

        }

        // Helper method to map Entity to DTO
        public static IEnumerable<BookingResponseDto> MapBookingEntitiesToDtos(IEnumerable<Bookings> bookings)
        {
            return bookings.Select(b => new BookingResponseDto
            {
                BookingId = b.Id,
                EndBooking = b.endBooking,
                StartBooking = b.startBooking,
                Status = b.Status,
            });
        }
    }
}
