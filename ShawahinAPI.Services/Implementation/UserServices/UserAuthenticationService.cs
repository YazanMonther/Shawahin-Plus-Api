﻿using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
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
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserLoginRepository _authenticationRepository;
        private readonly IUserGetRepository _getUserRepository;
        private readonly ITokenService _tokenService; 

        public UserAuthenticationService(IUserLoginRepository authenticationRepository, IUserGetRepository getUserRepository, ITokenService tokenService)
        {
            _authenticationRepository = authenticationRepository;
            _getUserRepository = getUserRepository;
            _tokenService = tokenService;
        }

        public async Task<UserAuthenticationResultDto> AuthenticateAsync(UserLoginDto loginDto)
        {
            // Validate the input
            var validationContext = new ValidationContext(loginDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(loginDto, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                var errors = validationResults.Select(r => r.ErrorMessage);
                throw new ValidationException(string.Join(Environment.NewLine, errors));
            }


            // Authenticate the user
            var user = await _authenticationRepository.LoginAsync(loginDto);

            if (user == null)
            {
                // Handle authentication failure here
                throw new Exception("Authentication failed.");
            }

            // Generate an access token
            var accessToken = _tokenService.GenerateToken(user);

            if (loginDto.Email == null)
            {
                // Handle authentication failure here
                throw new Exception("Authentication failed.");
            }

            // Get the user's profile
            var userProfile = await _getUserRepository.GetUserByEmailAsync(loginDto.Email);

            if (userProfile == null)
            {
                // Handle authentication failure here
                throw new Exception("Authentication failed.");
            }

            // Create the result DTO
            var result = UserAuthenticationMapper.MapToUserAuthenticationResultDto(accessToken, userProfile);
            
            return result;
        } 
    }
}