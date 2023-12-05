using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Core.Mappers.ChargingStationsMapper;
using ShawahinAPI.Services.Contract.IChargingStationsServices;


namespace ShawahinAPI.Services.Implementation
{
    public class ChargerStationCommentsService : IChargerStationCommentsService
    {
        private readonly IRepository<ChargerStationComments> _repository;

        public ChargerStationCommentsService(IRepository<ChargerStationComments> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ResultDto> AddCommentAsync(ChargerStationCommentBaseDto commentDto)
        {
            var comment = EntityDtoMapper<ChargerStationComments, ChargerStationCommentBaseDto>.MapToEntity(commentDto);

            return await _repository.AddAsync(comment);
        }

        public async Task<IEnumerable<ChargerStationCommentResponeDto>> GetCommentsForStationAsync(Guid stationId)
        {
            var comments = await _repository.GetByConditionAsync(c => c.StationId == stationId);

            if (comments != null)
            {
                // Map entities to DTOs and filter out null values
                var commentDtos = 
                     EntityDtoMapper<ChargerStationComments,
                    ChargerStationCommentResponeDto>.
                    MapToDto(comments);
                    

                return commentDtos;
            }

            // Return an empty collection if comments is null
            return Enumerable.Empty<ChargerStationCommentResponeDto>();
        }

        public async Task<ResultDto> RemoveCommentAsync(Guid commentId)
        {
            var comment = await _repository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return new ResultDto { Succeeded = false, Message = "Comment not found." };
            }

            return await _repository.RemoveAsync(comment);
        }

        public async Task<ResultDto> UpdateCommentAsync(ChargerStationCommentResponeDto commentDto)
        {
            var existingComment = await _repository.GetByIdAsync(commentDto.Id);
            if (existingComment == null)
            {
                return new ResultDto { Succeeded = false, Message = "Comment not found." };
            }

            EntityDtoMapper<ChargerStationComments, ChargerStationCommentResponeDto>.MapToEntity(commentDto, existingComment);

            return await _repository.UpdateAsync(existingComment);
        }

        public async Task<ChargerStationCommentBaseDto?> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _repository.GetByIdAsync(commentId);
            return comment != null ? EntityDtoMapper<ChargerStationComments, ChargerStationCommentBaseDto>.MapToDto(comment) : null;
        }
    }
}

