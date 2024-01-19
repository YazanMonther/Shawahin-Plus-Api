using Microsoft.AspNetCore.Identity;
using Moq;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories.IUserRepository;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers.UserMappers;
using ShawahinAPI.Persistence.Repository.UserRepositories.UserAuthRepositories;
using ShawahinAPI.Services.Contract.IUserServices;
using ShawahinAPI.Services.Implementation.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ShawainTestUnit
{

    /// <summary>
    /// Unit tests for user authentication-related services.
    /// </summary>
    public class UserAuthenticationTests
    {
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserSignOutService _signOutService;
        private readonly IUserRegistrationService _registrationService;
        private readonly Mock<IUserLoginRepository> _userLoginRepository;
        private readonly Mock<IUserGetRepository> _userGetRepository;
        private readonly Mock<ITokenService> _tokenService;
        private readonly Mock<IUserRegistrationRepository> _registrationsRepository;
        private readonly ITestOutputHelper _output;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;

        public UserAuthenticationTests(ITestOutputHelper output)
        {
            _output = output;
            _userLoginRepository = new Mock<IUserLoginRepository>();
            _userGetRepository = new Mock<IUserGetRepository>();
            _tokenService = new Mock<ITokenService>();
            _registrationsRepository = new Mock<IUserRegistrationRepository>();

            // Mock UserManager
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);

            // Authentication Service
            _authenticationService = new UserAuthenticationService(
                _userManagerMock.Object,
                _userLoginRepository.Object,
                _userGetRepository.Object,
                _tokenService.Object
            );

            // Sign Out Service
            _signOutService = new UserSignOutService(Mock.Of<IUserSignOutRepository>());

            // Registration Service
            _registrationService = new UserRegistrationService(_userManagerMock.Object, _registrationsRepository.Object);
        }
        #region SignIn Tests

        ///// <summary>
        ///// Should sign in user successfully.
        ///// </summary>
        //[Fact]
        //public async Task ShouldSignInUser_Success()
        //{
        //    // Arrange
        //    var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "Test@123" };
        //    var user = new ApplicationUser()
        //    {
        //        Id = Guid.NewGuid(),
        //        Email = "test@example.com",
        //        Fname = "test",
        //        Lname = "test",
        //        UserName = "test test"
        //    };
        //    _userLoginRepository.Setup(repo => repo.LoginAsync(userLoginDto))
        //        .ReturnsAsync(user);
        //    _userGetRepository.Setup(repo => repo.GetUserByEmailAsync(userLoginDto.Email))
        //        .ReturnsAsync(user);
        //    var role = new[]
        //    {
        //        "Admin",

        //    };
        //    _tokenService.Setup(repo => repo.GenerateToken(user, role.ToList())).
        //        Returns("Token");
        //    // Act
        //   var _authenticationServiceNew = new UserAuthenticationService(
        //            _userLoginRepository.Object,
        //            _userGetRepository.Object,
        //            _tokenService.Object,
        //        );
        //    var result = await _authenticationServiceNew.AuthenticateAsync(userLoginDto);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(result.ToString(), UserAuthenticationMapper.MapToUserAuthenticationResultDto("Token", user).ToString());
        //}

        /// <summary>
        /// Should handle authentication failure when user is not found.
        /// </summary>
        [Fact]
        public async Task ShouldHandleAuthenticationFailure_UserNotFound()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Email = "nonexistent@example.com", Password = "Test@123" };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authenticationService.AuthenticateAsync(userLoginDto));
        }

        /// <summary>
        /// Should handle authentication failure when password is incorrect.
        /// </summary>
        [Fact]
        public async Task ShouldHandleAuthenticationFailure_IncorrectPassword()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "IncorrectPassword" };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authenticationService.AuthenticateAsync(userLoginDto));
        }

        #endregion

        #region SignOut Tests

        /// <summary>
        /// Should sign out user successfully.
        /// </summary>
        [Fact]
        public async Task ShouldSignOutUser_Success()
        {
            // Act
            await _signOutService.SignOutAsync();

            // Assert
            
        }

        #endregion

        #region Registration Tests

        /// <summary>
        /// Should register user successfully.
        /// </summary>

        [Fact]
        public async Task RegisterAsync_SuccessfulRegistration_ReturnsSuccessResult()
        {
            // Arrange
            var registrationDto = new UserRegistrationDto
            {
                Email = "newuser@example.com",
                Password = "Test@123",
                Fname = "new",
                Lname = "user",
                PhoneNumber = "07940511223",
                UserRole = UserRole.User
            };

            _userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null); // Simulate no existing user

            _registrationsRepository.Setup(repo => repo.RegisterAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ResultDto { Succeeded = true, Message = "Registration successful." });

            // Act
            var _registrationServiceNew = new UserRegistrationService(_userManagerMock.Object, _registrationsRepository.Object);
            var result = await _registrationServiceNew.RegisterAsync(registrationDto);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("Registration successful.", result.Message);
        }



        #endregion

        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object);
        }
    }
}
