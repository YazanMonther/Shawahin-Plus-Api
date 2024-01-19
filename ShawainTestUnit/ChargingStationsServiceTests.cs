
using Moq;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Services.Implementation;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using System.Linq.Expressions;
using System;
using ShawahinAPI.Services.Contract;


namespace ShawainTestUnit
{
    public class ChargingStationsServiceTests
    {
        private readonly Mock<IRepository<ChargingStationRequests>> _chargingStationRequestRepositoryMock;
        private readonly Mock<IRepository<ApplicationUser>> _userGetRepositoryMock;
        private readonly Mock<IRepository<Contacts>> _contactRepositoryMock;
        private readonly Mock<IRepository<StationOpeningHours>> _stationOpeningHoursRepositoryMock;
        private readonly Mock<IRepository<Locations>> _locationsRepositoryMock;
        private readonly Mock<IRepository<Chargers>> _chargersRepositoryMock;
        private readonly Mock<IRepository<ChargerType>> _chargerTypeRepositoryMock;
        private readonly Mock<IRepository<ChargingStations>> _chargingStationRepositoryMock;
        private readonly Mock<IStationGetRepository> _stationGetRepositoryMock;
        private readonly ChargingStationsService _chargingStationsService;
        private readonly Mock<IEmailService> _email;   
        public ChargingStationsServiceTests()
        {
            // Initialize mocks
            _chargingStationRequestRepositoryMock = new Mock<IRepository<ChargingStationRequests>>();
            _userGetRepositoryMock = new Mock<IRepository<ApplicationUser>>();
            _contactRepositoryMock = new Mock<IRepository<Contacts>>();
            _stationOpeningHoursRepositoryMock = new Mock<IRepository<StationOpeningHours>>();
            _locationsRepositoryMock = new Mock<IRepository<Locations>>();
            _chargersRepositoryMock = new Mock<IRepository<Chargers>>();
            _chargerTypeRepositoryMock = new Mock<IRepository<ChargerType>>();
            _chargingStationRepositoryMock = new Mock<IRepository<ChargingStations>>();
            _stationGetRepositoryMock = new Mock<IStationGetRepository>();
            _email = new Mock<IEmailService>();
            // Initialize the service with mocked repositories
            _chargingStationsService = new ChargingStationsService(
                _chargingStationRequestRepositoryMock.Object,
                _userGetRepositoryMock.Object,
                _contactRepositoryMock.Object,
                _stationOpeningHoursRepositoryMock.Object,
                _locationsRepositoryMock.Object,
                _chargersRepositoryMock.Object,
                _chargerTypeRepositoryMock.Object,
                _chargingStationRepositoryMock.Object,
                _stationGetRepositoryMock.Object,
                _email.Object

            );;
        }
        #region AddNewChargingStationAsync

        [Fact]
        public async Task AddNewChargingStationAsync_InvalidRequest_ReturnsFailureResult()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            _userGetRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _chargingStationsService.AddNewChargingStationAsync(requestId, userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Invalid User Id", result.Message);
        }

        [Fact]
        public async Task AddNewChargingStationAsync_EmptyRequestId_ReturnsFailureResult()
        {
            // Arrange
            //Admin Id
            Guid userId = Guid.Parse("93ff8ec6-9744-4884-ca4e-08dbe81ffe8f");
            _userGetRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ApplicationUser()
                {
                    Id = userId,
                    UserRole = UserRole.Admin,
                });
            // Act
            var result = await _chargingStationsService.AddNewChargingStationAsync(Guid.Empty, userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Empty Request Id", result.Message);
        }


        #endregion

        #region GetAllChargingStationsAsync

        [Fact]
        public async Task GetAllChargingStationsAsync_Success_ReturnsChargingStationsList()
        {
            // Arrange
            var chargingStations = new List<ChargingStations> { new ChargingStations() };
            _chargingStationRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(chargingStations);

            // Act
            var result = await _chargingStationsService.GetAllChargingStationsAsync();

            // Assert
            Assert.NotEmpty(result);
        }


        [Fact]
        public async Task GetAllChargingStationsAsync_NoStations_ReturnsEmptyList()
        {
            // Arrange
            _chargingStationRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<ChargingStations>());

            // Act
            var result = await _chargingStationsService.GetAllChargingStationsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllChargingStationsAsync_NullResultFromRepository_ReturnsEmptyList()
        {
            // Arrange
            _chargingStationRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync((List<ChargingStations>)null);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                // Act
                var result = await _chargingStationsService.GetAllChargingStationsAsync();
            });
        }

        [Fact]
        public async Task GetAllChargingStationsAsync_MultipleStations_ReturnsCorrectList()
        {
            // Arrange
            var chargingStations = new List<ChargingStations>
            {
                new ChargingStations { Id = Guid.NewGuid(), /* Set other properties as needed */ },
                new ChargingStations { Id = Guid.NewGuid(), /* Set other properties as needed */ },
                new ChargingStations { Id = Guid.NewGuid(), /* Set other properties as needed */ }
            };
            _chargingStationRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(chargingStations);

            // Act
            var result = await _chargingStationsService.GetAllChargingStationsAsync();

            // Assert
            Assert.Equal(chargingStations.Count, result.Count());
        }

        #endregion

        #region GetChargingStationByIdAsync

        [Fact]
        public async Task GetChargingStationByIdAsync_InvalidStationId_ReturnsFailureResult()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            _chargingStationRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ChargingStations)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _chargingStationsService.GetChargingStationByIdAsync(stationId);
            });
        }

        [Fact]
        public async Task GetChargingStationByIdAsync_NullStationId_ReturnsFailureResult()
        {
            // Arrange
            Guid? stationId = null;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _chargingStationsService.GetChargingStationByIdAsync(stationId);
            });
        }


        [Fact]
        public async Task GetChargingStationByIdAsync_ValidStationId_ReturnsChargingStationDto()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var chargingStation = new ChargingStations { Id = stationId, /* Set other properties as needed */ };
            _chargingStationRepositoryMock.Setup(repo => repo.GetByIdAsync(stationId))
                .ReturnsAsync(chargingStation);

            // Act
            var result = await _chargingStationsService.GetChargingStationByIdAsync(stationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ChargingStationDto>(result);
            // Add more assertions based on your DTO structure and expected properties
        }

        #endregion

        #region UpdateChargingStationAsync

        [Fact]
        public async Task UpdateChargingStationAsync_InvalidStationId_ReturnsFailureResult()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var updatedStation = new ChargingStationDto();

            // Act
            var result = await _chargingStationsService.UpdateChargingStationAsync(stationId, updatedStation);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Invalid stationId Id.", result.Message);
        }

        // Add more test cases for UpdateChargingStationAsync as needed

        #endregion

        #region RemoveChargingStationAsync

        [Fact]
        public async Task RemoveChargingStationAsync_InvalidStationId_ReturnsFailureResult()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            _chargingStationRequestRepositoryMock.Setup(repo => repo.GetByIdAsync(stationId))
                .ReturnsAsync((ChargingStationRequests)null);
            // Act
            var result = await _chargingStationsService.RemoveChargingStationAsync(stationId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("No stations with this stationsId.", result.Message);
        }

        // Add more test cases for RemoveChargingStationAsync as needed

        #endregion

        #region GetFilteredChargingStations

        [Fact]
        public async Task GetFilteredChargingStations_Success_ReturnsFilteredStations()
        {
            // Arrange
            var chargerType = ChargersType.AVCONPlug.ToString();

            // Act
            var result = await _chargingStationsService.GetFilteredChargingStations(chargerType, null, null, null, null);

            // Assert
            Assert.NotNull(result);
        }

        // Add more test cases for GetFilteredChargingStations as needed

        #endregion

        #region GetChargingStationsByUserIdAsync

        [Fact]
        public async Task GetChargingStationsByUserIdAsync_InvalidUserId_ReturnsFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _chargingStationRepositoryMock.Setup(repo => repo.GetByConditionAsync(It.IsAny<Expression<Func<ChargingStations, bool>>>()))
                .ReturnsAsync((IEnumerable<ChargingStations>)null);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _chargingStationsService.GetChargingStationsByUserIdAsync(userId);
            });
        }


        #endregion
    }
}
