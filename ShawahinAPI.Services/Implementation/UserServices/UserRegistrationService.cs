using ShawahinAPI.Core.DTO.UserDTO;
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

        public UserRegistrationService(IUserRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
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
                return new ResultDto { Succeeded = false,  Message = string.Join(", ", errors) };
            }

            var user = UserRegistrationMapper.MapToUserAuthenticationResultDto(registrationDto);

            return await _registrationRepository.RegisterAsync(user,registrationDto.Password);
        }
    }
}
