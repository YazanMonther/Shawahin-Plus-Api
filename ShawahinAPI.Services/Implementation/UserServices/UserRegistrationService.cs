using Microsoft.AspNetCore.Identity;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IUserRepository;
using ShawahinAPI.Core.Mappers.UserMappers;
using ShawahinAPI.Services.Contract.IUserServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Implementation.UserServices
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRegistrationRepository _registrationRepository;
        private readonly UserManager<ApplicationUser> _userManager; // Replace YourUserType with your actual user type

        public UserRegistrationService(UserManager<ApplicationUser> userManager,IUserRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
            _userManager = userManager;
        }

        public async Task<ResultDto> RegisterAsync(UserRegistrationDto registrationDto)
        {
            // Validate the input
            var validationContext = new ValidationContext(registrationDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(registrationDto, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                var errors = validationResults.Select(r => r.ErrorMessage);
                return new ResultDto { Succeeded = false, Message = string.Join(", ", errors) };
            }

            // Check if the email already exists
            var existingUser = await _userManager.FindByEmailAsync(registrationDto.Email!);
        
            if (existingUser != null)
            {
                return new ResultDto { Succeeded = false, Message = "Email already exists." };
            }

            // Map DTO to user entity
            var user = UserRegistrationMapper.MapToUserAuthenticationResultDto(registrationDto);

            // Call registration repository to register user
            return await _registrationRepository.RegisterAsync(user, registrationDto.Password, registrationDto.UserRole.ToString());
        }
    }
}
