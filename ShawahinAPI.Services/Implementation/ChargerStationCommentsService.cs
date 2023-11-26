using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Implementation.Helpers;

namespace ShawahinAPI.Services.Implementation
{
    public class ChargerStationCommentsService : IChargerStationCommentsService
    {
        private readonly IChargerStationCommentsRepository _repository;

        public ChargerStationCommentsService(IChargerStationCommentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultDto> AddCommentAsync(ChargerStationCommentBaseDto commentDto)
        {
            var comment = ChargerStationCommentHelper.MapDtoToEntity(commentDto);

            return await _repository.AddCommentAsync(comment);
        }

        public async Task<IEnumerable<ChargerStationCommentResponeDto>> GetCommentsForStationAsync(Guid stationId)
        {
            var comments = await _repository.GetCommentsForStationAsync(stationId);
            return comments.Select(ChargerStationCommentHelper.MapEntityToDto);
        }

        public async Task<ResultDto> RemoveCommentAsync(Guid commentId)
        {
            var comment = await _repository.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                return new ResultDto { Succeeded = false, Message = "Comment not found." };
            }

            return await _repository.RemoveCommentAsync(comment);
        }

        public async Task<ResultDto> UpdateCommentAsync(ChargerStationCommentResponeDto commentDto)
        {
            var existingComment = await _repository.GetCommentByIdAsync(commentDto.Id);
            if (existingComment == null)
            {
                return new ResultDto { Succeeded = false, Message = "Comment not found." };
            }

            var updatedComment = ChargerStationCommentHelper.MapDtoToEntity(commentDto);
            updatedComment.Id = existingComment.Id;

            return await _repository.UpdateCommentAsync(updatedComment);
        }

        public async Task<ChargerStationCommentBaseDto?> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _repository.GetCommentByIdAsync(commentId);
            return comment != null ? ChargerStationCommentHelper.MapEntityToDto(comment) : null;
        }

    }
}
