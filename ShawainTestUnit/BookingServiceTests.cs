using System.Linq.Expressions;
using Moq;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Services.Contract;
using ShawahinAPI.Services.Implementation.ChargingStationServices;

namespace ShawainTestUnit
{

    public class BookingServiceTests
    {
        private readonly Mock<IRepository<Bookings>> _bookingRepositoryMock;
        private readonly Mock<IRepository<ChargingStations>> _stationsRepositoryMock;
        private readonly Mock<IRepository<Chargers>> _chargersRepositoryMock;
        private readonly Mock<IEmailService> _emailMockService;
        private readonly Mock<IUserGetRepository> _userMockGetRepository;
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            // Initialize mocks
            _bookingRepositoryMock = new Mock<IRepository<Bookings>>();
            _stationsRepositoryMock = new Mock<IRepository<ChargingStations>>();
            _chargersRepositoryMock = new Mock<IRepository<Chargers>>();
            _userMockGetRepository = new Mock<IUserGetRepository>();
            _emailMockService = new Mock<IEmailService>();
            // Initialize the service with mocked repositories
            _bookingService = new BookingService(_bookingRepositoryMock.Object, 
                _stationsRepositoryMock.Object, _chargersRepositoryMock.Object
                ,_userMockGetRepository.Object,_emailMockService.Object);
        }

        #region CreateBookingRequestAsync

        [Fact]
        public async Task CreateBookingRequestAsync_UserCantBookHisOwnStation_ReturnsFalse()
        {
            // Arrange
            var requestDto = new BookingRequestDto
            {
                StationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };

            _stationsRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ChargingStations { UserId = requestDto.UserId });

            // Act
            var result = await _bookingService.CreateBookingRequestAsync(requestDto);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("User Cant book his own Stations.", result.Message);
        }

        [Fact]
        public async Task CreateBookingRequestAsync_StationAlreadyBookedAtThisTime_ReturnsFalse()
        {
            // Arrange
            var requestDto = new BookingRequestDto
            {
                StationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                StartBooking = DateTime.Now,
            };

            var bookedEntities = new List<Bookings>
        {
            new Bookings
            {
                StationId = requestDto.StationId,
                startBooking = requestDto.StartBooking
            }
        };

            _stationsRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ChargingStations { UserId = Guid.NewGuid() });

            _bookingRepositoryMock.Setup(repo => repo.GetByConditionAsync(It.IsAny<Expression<Func<Bookings, bool>>>()))
                .ReturnsAsync(bookedEntities);


            // Act
            var result = await _bookingService.CreateBookingRequestAsync(requestDto);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Station already booked at this time.", result.Message);
        }

        // Add more test cases for CreateBookingRequestAsync as needed

        #endregion

        #region AcceptBookingAsync

        [Fact]
        public async Task AcceptBookingAsync_BookingNotFound_ReturnsFalse()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _bookingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Bookings)null);

            // Act
            var result = await _bookingService.AcceptBookingAsync(bookingId, userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Booking not found.", result.Message);
        }

        [Fact]
        public async Task AcceptBookingAsync_StationNotFound_ReturnsFalse()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _bookingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Bookings { StationId = Guid.NewGuid() });

            _stationsRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ChargingStations)null);

            // Act
            var result = await _bookingService.AcceptBookingAsync(bookingId, userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Station is not exist. ", result.Message);
        }

        #endregion

        #region DenyBookingAsync

        [Fact]
        public async Task DenyBookingAsync_BookingNotFound_ReturnsFalse()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _bookingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Bookings)null);

            // Act
            var result = await _bookingService.DenyBookingAsync(bookingId, userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Booking not found.", result.Message);
        }

        [Fact]
        public async Task DenyBookingAsync_StationNotFound_ReturnsFalse()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _bookingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Bookings { StationId = Guid.NewGuid() });

            _stationsRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ChargingStations )null);

            // Act
            var result = await _bookingService.DenyBookingAsync(bookingId, userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Station is not exist. ", result.Message);
        }

        #endregion

        #region GetBookingsByUserIdAsync

        [Fact]
        public async Task GetBookingsByUserIdAsync_NoBookings_ReturnsEmptyList()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _bookingRepositoryMock.Setup(repo => repo.GetByConditionAsync(It.IsAny<Expression<Func<Bookings, bool>>>()))
                .ReturnsAsync((IEnumerable<Bookings>)null);

            // Act
            var result = await _bookingService.GetBookingsByUserIdAsync(userId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBookingsByUserIdAsync_WithBookings_ReturnsBookingResponseDtos()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bookingsList = new List<Bookings> { new Bookings() }; // Add a booking to the list

            _bookingRepositoryMock.Setup(repo => repo.GetByConditionAsync(It.IsAny<Expression<Func<Bookings, bool>>>()))
                .ReturnsAsync(bookingsList);

            // Act
            var result = await _bookingService.GetBookingsByUserIdAsync(userId);

            // Assert
            Assert.NotEmpty(result);
        }


        #endregion


        #region GetBookingsByStationIdAsync

        [Fact]
        public async Task GetBookingsByStationIdAsync_NoBookings_ReturnsEmptyList()
        {
            // Arrange
            var stationId = Guid.NewGuid();

            _bookingRepositoryMock.Setup(repo => repo.GetByConditionAsync(It.IsAny<Expression<Func<Bookings, bool>>>()))
                .ReturnsAsync((IEnumerable<Bookings>)null);

            // Act
            var result = await _bookingService.GetBookingsByStationIdAsync(stationId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBookingsByStationIdAsync_WithBookings_ReturnsBookingResponseDtos()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var bookingsList = new List<Bookings> { new Bookings() }; // Add a booking to the list

            _bookingRepositoryMock.Setup(repo => repo.GetByConditionAsync(It.IsAny<Expression<Func<Bookings, bool>>>()))
                .ReturnsAsync(bookingsList);

            // Act
            var result = await _bookingService.GetBookingsByStationIdAsync(stationId);

            // Assert
            Assert.NotEmpty(result);
        }

        #endregion





    }

}
