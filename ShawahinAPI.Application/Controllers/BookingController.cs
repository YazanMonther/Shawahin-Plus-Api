using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        [HttpPost("createBookingRequest")]
        public async Task<IActionResult> CreateBookingRequest([FromBody] BookingRequestDto requestDto)
        {
            var result = await _bookingService.CreateBookingRequestAsync(requestDto);

            return result.Succeeded ? (IActionResult)Ok(result) : BadRequest(result);
        }

        [HttpPost("acceptBooking/{bookingId}/{UserId}")]
        public async Task<IActionResult> AcceptBooking(Guid bookingId, Guid UserId)
        {
            var result = await _bookingService.AcceptBookingAsync(bookingId,UserId);

            return result.Succeeded ? (IActionResult)Ok(result) : BadRequest(result);
        }

        [HttpPost("denyBooking/{bookingId}/{UserId}")]
        public async Task<IActionResult> DenyBooking(Guid bookingId, Guid UserId)
        {
            var result = await _bookingService.DenyBookingAsync(bookingId, UserId);

            return result.Succeeded ? (IActionResult)Ok(result) : BadRequest(result);
        }

        [HttpGet("getBookingsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookingsByUserId(Guid userId)
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);

            return Ok(bookings);
        }

        [HttpGet("getBookingsByStationId/{stationId}")]
        public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookingsByStationId(Guid stationId)
        {
            var bookings = await _bookingService.GetBookingsByStationIdAsync(stationId);

            return Ok(bookings);
        }

        [HttpGet("getBookingsByOwnerUserId/{ownerUserId}")]
        public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookingsByOwnerUserId(Guid ownerUserId)
        {
            var bookings = await _bookingService.GetBookingsByOwnerUserIdAsync(ownerUserId);

            return Ok(bookings);
        }
    }
}
