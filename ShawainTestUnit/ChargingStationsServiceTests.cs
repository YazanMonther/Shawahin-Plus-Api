
//using Moq;
//using ShawahinAPI.Core.DTO.UserDTO;
//using ShawahinAPI.Core.Entities;
//using ShawahinAPI.Core.Entities.ChargingStationsEntities;
//using ShawahinAPI.Core.Enums;
//using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories.IChargersRepositories;
//using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories.IChargingStationRepositories;
//using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories.IChargingStationRequestsRepositories;
//using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories.IContactRepositories;
//using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories.ILocationsRepositories;
//using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories.IStationOpeningHoursRepository;
//using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
//using ShawahinAPI.Services.Implementation.ChargingStationsService;

//namespace ShawainTestUnit
//{
//    public class ChargingStationsServiceTests 
//    {
//        private readonly Mock<IChargingStationRequestGetByIdRepository> _byIdRepositoryMock;
//        private readonly Mock<IChargingStationAddRepository> _addRepositoryMock;
//        private readonly Mock<IUserGetRepository> _userGetRepositoryMock;
//        private readonly Mock<IContactGetByIdRepository> _contactGetByIdRepositoryMock;
//        private readonly Mock<IStationOpeningHoursGetByIdRepository> _stationOpeningHoursGetRepositoryMock;
//        private readonly Mock<ILocationsGetByIdRepository> _locationsByIdRepositoryMock;
//        private readonly Mock<IChargersGetByIdRepository> _chargersGetByIdRepositoryMock;
//        private readonly Mock<IChargingStationRequestUpdateStatusRepository> _requestUpdateMock;
//        public ChargingStationsServiceTests()
//        {
//            _addRepositoryMock = new Mock<IChargingStationAddRepository>();
//            _byIdRepositoryMock = new Mock<IChargingStationRequestGetByIdRepository>();
//            _userGetRepositoryMock = new Mock<IUserGetRepository>();
//            _contactGetByIdRepositoryMock = new Mock<IContactGetByIdRepository>();
//            _stationOpeningHoursGetRepositoryMock = new Mock<IStationOpeningHoursGetByIdRepository>();
//            _locationsByIdRepositoryMock = new Mock<ILocationsGetByIdRepository>();
//            _chargersGetByIdRepositoryMock = new Mock<IChargersGetByIdRepository>();
//            _requestUpdateMock = new Mock<IChargingStationRequestUpdateStatusRepository>();
//        }

//        #region ValidRequest
//        /// <summary>
//        /// For valid id request
//        /// </summary>
//        /// <returns>it should return a true and ("Station added successfully) Message</returns>
//        [Fact]
//        public async Task AddNewChargingStationAsync_ValidRequest_ReturnsSuccessResult()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var userId = Guid.NewGuid();
//            var contactId = Guid.NewGuid();
//            var stationOpeningHoursId = Guid.NewGuid();
//            var locationId = Guid.NewGuid();
//            var chargerId = Guid.NewGuid();
//            var chargingStationRequest = new ChargingStationRequests();

//            _byIdRepositoryMock.Setup(repo => repo.GetRequestByIdAsync(requestId))
//                .ReturnsAsync(chargingStationRequest);

//            _addRepositoryMock.Setup(repo => repo.AddStationAsync(It.IsAny<ChargingStations>()))
//                .ReturnsAsync(new ResultDto { Succeeded = true, Message = "Station added successfully" });

//            _userGetRepositoryMock.Setup(repo => repo.GetUserByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new ApplicationUser()
//                {
//                    Id = userId ,
//                    UserRole = UserRole.Admin,
//                    Email = "Yazanmonther132@gmail.com",
//                    Fname = "Yazan",
//                    Lname = "Alhelo"
//                }); ;
//            _contactGetByIdRepositoryMock.Setup(repo => repo.GetContactByIdAsync(It.IsAny<Guid>())).
//                ReturnsAsync(new Contacts()
//                {
//                    Id = contactId,
//                    Email = "Yazan@gmail.com",
//                    Name = "Yazan",
//                    Phone ="+974232324"
//                });
//            // Set up behavior for StationOpeningHours repository mock
//            _stationOpeningHoursGetRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new StationOpeningHours
//                {
//                    // Set the properties according to your needs
//                    Id = stationOpeningHoursId,
//                    // ...
//                });

//            // Set up behavior for Locations repository mock
//            _locationsByIdRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new Locations
//                {
//                    Id = locationId,
//                });

//            // Set up behavior for Chargers repository mock
//            _chargersGetByIdRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new Chargers
//                {
//                    Id = chargerId,
//                });
//            _requestUpdateMock.Setup(repo => repo.UpdateRequestStatusAsync(It.IsAny<Guid>(),
//                RequestStatus.Accepted)).ReturnsAsync(new ResultDto()
//                {
//                    Succeeded = true
//                });


//            var service = new AddChargingStationsService(_byIdRepositoryMock.Object,
//                _addRepositoryMock.Object, _userGetRepositoryMock.Object,
//                _contactGetByIdRepositoryMock.Object,_stationOpeningHoursGetRepositoryMock.Object,
//                _locationsByIdRepositoryMock.Object, _chargersGetByIdRepositoryMock.Object,
//                _requestUpdateMock.Object);

//            // Act
//            var result = await service.AddNewChargingStationAsync(requestId,userId);

//            // Assert
//            Assert.NotNull(result); // Example assertion
//            Assert.True(result.Succeeded);
//            Assert.Equal("Station added successfully", result.Message);


//            // Add more specific assertions if needed

//        }
//        #endregion

//        #region EmptyIdRequest
//        /// <summary>
//        /// supply Empty request Id
//        /// </summary>
//        /// <returns>it should reutrn false and "Empty Request Id" message</returns>
//        [Fact]
//        public async Task AddNewChargingStationAsync_EmptyIdRequest_ReturnsFalseResult()
//        {
//            // Arrange
//            var requestId = Guid.Empty;
//            var userId = Guid.NewGuid();
//            var contactId = Guid.NewGuid();
//            var stationOpeningHoursId = Guid.NewGuid();
//            var locationId = Guid.NewGuid();
//            var chargerId = Guid.NewGuid();
//            var chargingStationRequest = new ChargingStationRequests();

//            _byIdRepositoryMock.Setup(repo => repo.GetRequestByIdAsync(requestId))
//                .ReturnsAsync(chargingStationRequest);

//            _addRepositoryMock.Setup(repo => repo.AddStationAsync(It.IsAny<ChargingStations>()))
//                .ReturnsAsync(new ResultDto { Succeeded = true, Message = "Station added successfully" });
//            _userGetRepositoryMock.Setup(repo => repo.GetUserByIdAsync(It.IsAny<Guid>()))
//            .ReturnsAsync(new ApplicationUser()
//            {
//                Id = userId,
//                UserRole = UserRole.Admin,
//                Email = "Yazanmonther132@gmail.com",
//                Fname = "Yazan",
//                Lname = "Alhelo"
//            }); ;
//            _contactGetByIdRepositoryMock.Setup(repo => repo.GetContactByIdAsync(It.IsAny<Guid>())).
//                ReturnsAsync(new Contacts()
//                {
//                    Id = contactId,
//                    Email = "Yazan@gmail.com",
//                    Name = "Yazan",
//                    Phone = "+974232324"
//                });
//            // Set up behavior for StationOpeningHours repository mock
//            _stationOpeningHoursGetRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new StationOpeningHours
//                {
//                    // Set the properties according to your needs
//                    Id = stationOpeningHoursId,
//                    // ...
//                });

//            // Set up behavior for Locations repository mock
//            _locationsByIdRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new Locations
//                {
//                    Id = locationId,
//                });

//            // Set up behavior for Chargers repository mock
//            _chargersGetByIdRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new Chargers
//                {
//                    Id = chargerId,
//                });

//            _requestUpdateMock.Setup(repo => repo.UpdateRequestStatusAsync(It.IsAny<Guid>(),
//                RequestStatus.Accepted)).ReturnsAsync(new ResultDto()
//                {
//                    Succeeded = true
//                });


//            var service = new AddChargingStationsService(_byIdRepositoryMock.Object,
//                _addRepositoryMock.Object, _userGetRepositoryMock.Object,
//                _contactGetByIdRepositoryMock.Object, _stationOpeningHoursGetRepositoryMock.Object,
//                _locationsByIdRepositoryMock.Object, _chargersGetByIdRepositoryMock.Object,
//                _requestUpdateMock.Object);
//            // Act
//            var result = await service.AddNewChargingStationAsync(requestId, userId);

//            // Assert
//            Assert.NotNull(result); // Example assertion
//            Assert.True(!result.Succeeded);
//            Assert.Equal("Empty Request Id", result.Message);
//        }
//        #endregion

//        #region WrongRequestId
//        /// <summary>
//        /// supply Wrong request Id (No request with this id)
//        /// </summary>
//        /// <returns>it should reutrn false and "Invalid Request Id"  message</returns>
//        [Fact]
//        public async Task AddNewChargingStationAsync_WrongRequestId_ReturnsFalseResult()
//        {
//            // Arrange
//            var requestId = Guid.NewGuid();
//            var userId = Guid.NewGuid();
//            var contactId = Guid.NewGuid();
//            var stationOpeningHoursId = Guid.NewGuid();
//            var locationId = Guid.NewGuid();
//            var chargerId = Guid.NewGuid();
//            ChargingStationRequests? chargingStationRequest = null;

//            _byIdRepositoryMock.Setup(repo => repo.GetRequestByIdAsync(requestId))
//                .ReturnsAsync(chargingStationRequest);

//            _addRepositoryMock.Setup(repo => repo.AddStationAsync(It.IsAny<ChargingStations>()))
//                .ReturnsAsync(new ResultDto { Succeeded = true, Message = "Station added successfully" });
//            _userGetRepositoryMock.Setup(repo => repo.GetUserByIdAsync(It.IsAny<Guid>()))
//            .ReturnsAsync(new ApplicationUser()
//            {
//                Id = userId,
//                UserRole = UserRole.Admin,
//                Email = "Yazanmonther132@gmail.com",
//                Fname = "Yazan",
//                Lname = "Alhelo"
//            }); ;
//            _contactGetByIdRepositoryMock.Setup(repo => repo.GetContactByIdAsync(It.IsAny<Guid>())).
//                ReturnsAsync(new Contacts()
//                {
//                    Id = contactId,
//                    Email = "Yazan@gmail.com",
//                    Name = "Yazan",
//                    Phone = "+974232324"
//                });
//            // Set up behavior for StationOpeningHours repository mock
//            _stationOpeningHoursGetRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new StationOpeningHours
//                {
//                    // Set the properties according to your needs
//                    Id = stationOpeningHoursId,
//                    // ...
//                });

//            // Set up behavior for Locations repository mock
//            _locationsByIdRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new Locations
//                {
//                    Id = locationId,
//                });

//            // Set up behavior for Chargers repository mock
//            _chargersGetByIdRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
//                .ReturnsAsync(new Chargers
//                {
//                    Id = chargerId,
//                });
//            _requestUpdateMock.Setup(repo => repo.UpdateRequestStatusAsync(It.IsAny<Guid>(),
//                RequestStatus.Accepted)).ReturnsAsync(new ResultDto()
//                {
//                    Succeeded = true
//                });


//            var service = new AddChargingStationsService(_byIdRepositoryMock.Object,
//                _addRepositoryMock.Object, _userGetRepositoryMock.Object,
//                _contactGetByIdRepositoryMock.Object, _stationOpeningHoursGetRepositoryMock.Object,
//                _locationsByIdRepositoryMock.Object, _chargersGetByIdRepositoryMock.Object,
//                _requestUpdateMock.Object);
//            // Act
//            var result = await service.AddNewChargingStationAsync(requestId, userId );

//            // Assert
//            Assert.NotNull(result); // Example assertion
//            Assert.True(!result.Succeeded);
//            Assert.Equal("Invalid Request Id", result.Message);
//        }
//        #endregion
//    }
//}
