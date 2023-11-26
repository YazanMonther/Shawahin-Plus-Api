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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthenticationTests"/> class.
        /// </summary>

        public UserAuthenticationTests(ITestOutputHelper output)
        {
            _output = output;
            _userLoginRepository = new Mock<IUserLoginRepository>();
            _userGetRepository = new Mock<IUserGetRepository>();
            _tokenService = new Mock<ITokenService>();
            _registrationsRepository = new Mock<IUserRegistrationRepository>();
            // Authentication Service
            _authenticationService = new UserAuthenticationService(
                _userLoginRepository.Object,
                _userGetRepository.Object,
                _tokenService.Object
            );

            // Sign Out Service
            _signOutService = new UserSignOutService(Mock.Of<IUserSignOutRepository>());

            // Registration Service
            _registrationService = new UserRegistrationService(_registrationsRepository.Object);
        }

        #region SignIn Tests

        /// <summary>
        /// Should sign in user successfully.
        /// </summary>
        [Fact]
        public async Task ShouldSignInUser_Success()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "Test@123" };
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Fname = "test",
                Lname = "test",
                UserName = "test test"
            };
            _userLoginRepository.Setup(repo => repo.LoginAsync(userLoginDto))
                .ReturnsAsync(user);
            _userGetRepository.Setup(repo => repo.GetUserByEmailAsync(userLoginDto.Email))
                .ReturnsAsync(user);
            _tokenService.Setup(repo => repo.GenerateToken(user)).
                Returns("Token");
            // Act
           var _authenticationServiceNew = new UserAuthenticationService(
                    _userLoginRepository.Object,
                    _userGetRepository.Object,
                    _tokenService.Object
                );
            var result = await _authenticationServiceNew.AuthenticateAsync(userLoginDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.ToString(), UserAuthenticationMapper.MapToUserAuthenticationResultDto("Token", user).ToString());
        }

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
        public async Task ShouldRegisterUser_Success()
        {
            // Arrange
            var userRegistrationDto = new UserRegistrationDto
            {
                Email = "newuser@example.com",
                Password = "Test@123",
                Fname = "new",
                Lname = "user",
                PhoneNumber = "07940511223",
                UserRole = UserRole.User
            };

            var resultDto = new ResultDto() { Succeeded = true, Message = "User Register Successfully" };

            _registrationsRepository.Setup(repo => repo.RegisterAsync(It.IsAny<ApplicationUser>(), userRegistrationDto.Password))
                .ReturnsAsync(resultDto);
           
            // Act
            var _registrationServiceNew = new UserRegistrationService(_registrationsRepository.Object);
            var result = await _registrationServiceNew.RegisterAsync(userRegistrationDto);

            // Output debug information
            if (result == null)
            {
                _output.WriteLine("Result is null");
            }
            else
            {
                _output.WriteLine(result.Message + " " + result.Succeeded);
            }

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.True(result.Succeeded == resultDto.Succeeded);
        }



        #endregion

        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object);
        }
    }
}
