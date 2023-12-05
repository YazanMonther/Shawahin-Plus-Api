using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers;

using ShawahinAPI.Services.Contract.ICommunityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Implementation.CommunityServices
{
    public class CommunityEventsService : ICommunityEventsService
    {
        private readonly IRepository<CommunityEvents> _repository;

        public CommunityEventsService(IRepository<CommunityEvents> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ResultDto> AddEventAsync(CommunityEventBaseDto? eventDto)
        {
            var communityEvent = EntityDtoMapper<CommunityEvents, CommunityEventBaseDto>.MapToEntity(eventDto);

            if (communityEvent == null)
            {
                return new ResultDto { Succeeded = false, Message = "Community Event not found." };
            }

            var result = await _repository.AddAsync(communityEvent);

            return result;
        }

        public async Task<IEnumerable<CommunityEventResponseDto?>> GetAllEventsAsync()
        {
            var events = await _repository.GetAllAsync();

            // Map entities to DTOs as needed
            var eventDtos = EntityDtoMapper<CommunityEvents, CommunityEventResponseDto>.MapToDto(events);

            return eventDtos;
        }

        public async Task<CommunityEventResponseDto?> GetEventByIdAsync(Guid eventId)
        {
            var communityEvent = await _repository.GetByIdAsync(eventId);

            if (communityEvent != null)
            {
                // Map entity to DTO as needed
                var eventDto = EntityDtoMapper<CommunityEvents, CommunityEventResponseDto>.MapToDto(communityEvent);
                return eventDto;
            }
            return null;
        }

        public async Task<ResultDto> RemoveEventAsync(Guid eventId)
        {
            var communityEvent = await _repository.GetByIdAsync(eventId);

            if (communityEvent == null)
            {
                return new ResultDto { Succeeded = false, Message = "Community Event not found." };
            }

            return await _repository.RemoveAsync(communityEvent);
        }

        public async Task<ResultDto> UpdateEventAsync(CommunityEventBaseDto? eventDto, Guid eventId)
        {
            // Check if the provided DTO is null
            if (eventDto == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid Event provider" };
            }

            // Retrieve the existing community event from the repository
            var existingEvent = await _repository.GetByIdAsync(eventId);

            // Check if the existing event is not found
            if (existingEvent == null)
            {
                return new ResultDto { Succeeded = false, Message = "Community Event not found" };
            }

            // Update the properties of the existing event with values from the DTO
            EntityDtoMapper< CommunityEvents , CommunityEventBaseDto>.MapToEntity(eventDto, existingEvent);

            // Call the repository to update the existing event
            return await _repository.UpdateAsync(existingEvent);
        }

    }
}
