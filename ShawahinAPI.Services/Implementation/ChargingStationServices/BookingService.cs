using ShawahinAPI.Core.DTO;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers.ChargingStationsMapper;
using ShawahinAPI.Services.Contract;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using System.Runtime.InteropServices;


namespace ShawahinAPI.Services.Implementation.ChargingStationServices
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Bookings> _bookingRepository;
        private readonly IRepository<ChargingStations> _statoinsRepository;
        private readonly IRepository<Chargers> _chargersRepository;
        private readonly IEmailService _emailService;
        private readonly IUserGetRepository _userGetRepository;

        public BookingService(IRepository<Bookings> bookingRepository,
            IRepository<ChargingStations> stations, IRepository<Chargers> chargersRepository,
             IUserGetRepository userGetRepository, IEmailService emailService)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            this._statoinsRepository = stations;
            this._chargersRepository = chargersRepository;
            this._emailService = emailService;
            this._userGetRepository = userGetRepository;
        }

        public async Task<ResultDto> CreateBookingRequestAsync(BookingRequestDto requestDto)
        {

            var station = await _statoinsRepository.GetByIdAsync(requestDto.StationId);

            if(station?.UserId == requestDto.UserId)
            {
                return new ResultDto { Succeeded = false, Message = "User Cant book his own Stations." };
            }
            // Check if the station is already booked at this time
            var isStationBooked = await _bookingRepository
                .GetByConditionAsync(b => b.StationId == requestDto.StationId &&
                                            b.startBooking == requestDto.StartBooking);


            if (isStationBooked.Any())
            {
                return new ResultDto { Succeeded = false, Message = "Station already booked at this time." };
            }

            // Map DTO to Entity
            var bookingEntity = ChargerBookingsMapper.MapDtoToEntity(requestDto);

            var addResult =  await _bookingRepository.AddAsync(bookingEntity);

            try
            {
                if (addResult.Succeeded)
                {
                    var User = await _userGetRepository.GetUserByIdAsync(requestDto.UserId);
                    var ownerUser = await getOwnerByStationId(requestDto.StationId);
                    var charger = await _chargersRepository.GetByIdAsync(station.ChargesId );
                    // Send booking request email to the station owner
                    EmailRequest bookingRequestEmail = new EmailRequest()
                    {
                        ToEmail = ownerUser?.Email!,
                        Body = $"You have received a booking request with {station}. " +
                        $"Please log in to your account to review and respond.",
                        Subject = "New Booking Request"
                    };

                    await _emailService.SendEmailAsync(bookingRequestEmail);

                    // Send booking request notification email to the user
                    EmailRequest userBookingRequestNotificationEmail = new EmailRequest()
                    {
                        ToEmail = User?.Email!,
                        Body = $"Thank you for your booking request with {charger?.ChargerName}." +
                        $" It is currently being reviewed by the station owner. " +
                        $"You will receive another email once your booking is accepted or denied.",
                        Subject = $"Booking Request with {charger?.ChargerName} Received"
                    };

                    await _emailService.SendEmailAsync(userBookingRequestNotificationEmail);
                }
            }
            catch (Exception ex)
            {

                return new ResultDto { Succeeded = addResult.Succeeded, Message = $"{addResult.Message}, Error sending email: {ex.Message}" };
            }

            return addResult;
        }

        public async Task<ResultDto> AcceptBookingAsync(Guid bookingId,Guid UserId)
        {

            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking == null)
            {
                return new ResultDto { Succeeded = false, Message = "Booking not found." };
            }
            var station = await _statoinsRepository.GetByIdAsync(booking.StationId);
            if(station == null)
            {
                return new ResultDto { Succeeded = false, Message = "Station is not exist. " };
            }
            if (station.UserId != UserId)
            {
                return new ResultDto { Succeeded = false, Message = "you arent the station Owner  " };
            }
            if (booking.Status != BookingStatus.Accepted.ToString())
            {
                var bookingResult = await UpdateBookingStatusAsync(bookingId, BookingStatus.Accepted);
                    if (bookingResult.Succeeded)
                    {
                        var User = await _userGetRepository.GetUserByIdAsync(booking.UserId);
                        var charger = await _chargersRepository.GetByIdAsync(station.ChargesId);
                    try
                    {
                            EmailRequest emailRequest = new EmailRequest()
                            {
                                ToEmail = User?.Email!,
                                Body = $"Your booking has been accepted with {charger?.ChargerName}. " +
                                $"We look forward to serving you!",
                                Subject = $"Booking with {charger?.ChargerName} Accepted"
                            };
                            await _emailService.SendEmailAsync(emailRequest);
                        }
                        catch (Exception ex)
                        {

                            return new ResultDto { Succeeded = bookingResult.Succeeded, Message = $"{bookingResult.Message}, Error sending email: {ex.Message}" };
                        }
                    }

                return bookingResult;
            }


            return new ResultDto { Succeeded = false, Message = "Booking is already accepted." };
        }


        public async Task<ResultDto> DenyBookingAsync(Guid bookingId,Guid UserId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking == null)
            {
                return new ResultDto { Succeeded = false, Message = "Booking not found." };
            }

            var station = await _statoinsRepository.GetByIdAsync(booking.StationId);
            if (station == null)
            {
                return new ResultDto { Succeeded = false, Message = "Station is not exist. " };
            }
            if (station.UserId != UserId)
            {
                return new ResultDto { Succeeded = false, Message = "you arent the station Owner  " };
            }
            if (booking.Status != BookingStatus.Denied.ToString())
            {
                var bookingResult = await UpdateBookingStatusAsync(bookingId, BookingStatus.Denied);
                try
                {
                    var charger = await _chargersRepository.GetByIdAsync(station.ChargesId);
                    if (bookingResult.Succeeded)
                    {
                        var User = await _userGetRepository.GetUserByIdAsync(booking.UserId);
                        EmailRequest bookingDeniedEmail = new EmailRequest()
                        {
                            ToEmail = User?.Email!,
                            Body = $"Unfortunately, your booking wiht {charger?.ChargerName} has been denied. " +
                            $"Please contact support for more information.",
                            Subject = $"Booking with {charger?.ChargerName} Denied"
                        };
                        await _emailService.SendEmailAsync(bookingDeniedEmail);
                    }
                }
                catch (Exception ex)
                {

                    return new ResultDto { Succeeded = bookingResult.Succeeded, Message = $"{bookingResult.Message}, Error sending email: {ex.Message}" };
                }
                return bookingResult;
            }

            return new ResultDto { Succeeded = false, Message = "Booking is already denied." };
        }


        public async Task<IEnumerable<BookingResponseDto>> GetBookingsByUserIdAsync(Guid userId)
        {
            var bookings = await _bookingRepository.GetByConditionAsync(b => b.UserId == userId);

            if (bookings == null)
            {
                // Handle the case where bookings is null (perhaps log or throw an exception)
                return Enumerable.Empty<BookingResponseDto>();
            }

            var bookingResponse = ChargerBookingsMapper.MapBookingEntitiesToDtos(bookings!);

            return await AddChargerNameToBookingsAsync(bookingResponse, bookings!);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetBookingsByStationIdAsync(Guid stationId)
        {
            var bookings = await _bookingRepository.GetByConditionAsync(b => b.StationId == stationId);
            if (bookings == null)
            {
                // Handle the case where bookings is null (perhaps log or throw an exception)
                return Enumerable.Empty<BookingResponseDto>();
            }
            var bookingResponse = ChargerBookingsMapper.MapBookingEntitiesToDtos(bookings!);

            return await AddChargerNameToBookingsAsync(bookingResponse, bookings!);
        }
    

        public async Task<IEnumerable<BookingResponseDto>> GetBookingsByOwnerUserIdAsync(Guid ownerUserId)
        {
            // Assuming the owner user ID is associated with stations
            var userStations = await _statoinsRepository
                .GetByConditionAsync(b => b.UserId == ownerUserId);
            List<Guid> userStationsIds = userStations.Select(s =>s!.Id).ToList();
            var ownerBookings = await _bookingRepository.GetByConditionAsync(b => userStationsIds.Contains(b.StationId));

            if (ownerBookings == null)
            {
                // Handle the case where bookings is null (perhaps log or throw an exception)
                return Enumerable.Empty<BookingResponseDto>();
            }
            var bookingResponse = ChargerBookingsMapper.MapBookingEntitiesToDtos(ownerBookings!);

            return await AddChargerNameToBookingsAsync(bookingResponse, ownerBookings!);
        }
    


        #region Helper Methods
        private async Task<ResultDto> UpdateBookingStatusAsync(Guid bookingId, BookingStatus status)
        {
            var bookingEntity = await _bookingRepository.GetByIdAsync(bookingId);

            if (bookingEntity == null)
            {
                return new ResultDto { Succeeded = false, Message = "Booking not found." };
            }

            // Update status
            bookingEntity.Status = status.ToString();

            return await _bookingRepository.UpdateAsync(bookingEntity);
        }


        private async Task<IEnumerable<BookingResponseDto>> AddChargerNameToBookingsAsync(IEnumerable<BookingResponseDto> bookingResposnse, IEnumerable<Bookings> bookings)
        {
            var bookingResponseDtos = new List<BookingResponseDto>();

            foreach (var item in bookingResposnse)
            {
                var stationId = bookings.FirstOrDefault(b => b.Id == item.BookingId)?.StationId;

                if (stationId != null)
                {
                    var station = await _statoinsRepository.GetByIdAsync(stationId.Value);
                    var chargerId = station?.ChargesId;

                    if (chargerId != null)
                    {
                        var charger = await _chargersRepository.GetByIdAsync(chargerId.Value);

                        if (charger != null)
                        {
                            item.ChargerName = charger.ChargerName;
                        }
                    }
                }

                bookingResponseDtos.Add(item);
            }

            return bookingResponseDtos;
        }

        private async Task<ApplicationUser?> getOwnerByStationId(Guid stationId)
        {
         var station = await  _statoinsRepository.GetByIdAsync(stationId);
            if (station?.UserId != null)
            {
                var ownerUser = await _userGetRepository.GetUserByIdAsync(station.UserId.Value);
                return ownerUser;
            }

            return null;
        }

        #endregion

    } 

}