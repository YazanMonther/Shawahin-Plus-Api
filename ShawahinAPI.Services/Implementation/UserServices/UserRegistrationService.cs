using Microsoft.AspNetCore.Identity;
using ShawahinAPI.Core.DTO;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IUserRepository;
using ShawahinAPI.Core.Mappers.UserMappers;
using ShawahinAPI.Services.Contract;
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
        private readonly IEmailService _emailService;
        public UserRegistrationService(UserManager<ApplicationUser> userManager,
                                       IUserRegistrationRepository registrationRepository,
                                       IEmailService emailService)
        {
            _registrationRepository = registrationRepository;
            _userManager = userManager;
            _emailService = emailService;
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

            // Register the user
            var result = await _registrationRepository.RegisterAsync(user, registrationDto.Password, registrationDto.UserRole.ToString());

            if (result.Succeeded)
            {
                // Send a welcome email
                var emailRequest = new EmailRequest
                {
                    ToEmail = registrationDto.Email!,
                    Subject = "Welcome to Shawahin!",
                    Body = @"
                        <div style='font-family: Arial, sans-serif;'>
                            <h1 style='color: #4CAF50;'>مرحباً بك في شواهين! | Welcome to Shawahin!</h1>
                            <p style='font-size: 16px;'>
                                شكراً لتسجيلك! نحن متحمسون لانضمامك إلينا. إذا كان لديك أي استفسار، لا تتردد في التواصل معنا.
                            </p>
                            <p style='font-size: 16px;'>
                                Thank you for registering! We are excited to have you onboard. If you have any questions, feel free to reach out.
                            </p>
                        </div>"
                };

                await _emailService.SendEmailAsync(emailRequest);
            }

            return result;
        }
    }
}
