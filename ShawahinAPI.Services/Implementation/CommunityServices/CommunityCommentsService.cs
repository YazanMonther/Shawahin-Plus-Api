using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract.ICommunityServices;


namespace ShawahinAPI.Services.Implementation.CommunityServices
{
    public class CommunityCommentsService : ICommunityCommentsService
    {
        private readonly IRepository<CommunityComments> _repository;

        public CommunityCommentsService(IRepository<CommunityComments> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ResultDto> AddCommentAsync(CommunityCommentBaseDto commentDto)
        {
            var comment = EntityDtoMapper<CommunityComments, CommunityCommentBaseDto>.MapToEntity(commentDto);

            var result = await _repository.AddAsync(comment);

            return result;
        }

        public async Task<IEnumerable<CommunityCommentBaseDto?>> GetAllCommentsAsync()
        {
            var comments = await _repository.GetAllAsync();

            // Map entities to DTOs as needed
            var commentDtos = EntityDtoMapper<CommunityComments, CommunityCommentBaseDto>.MapToDto(comments);

            return commentDtos;
        }

        public async Task<IEnumerable<CommunityCommentBaseDto?>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _repository.GetByConditionAsync(c => c.PostId == postId);

            // Map entities to DTOs as needed
            var commentDtos = EntityDtoMapper<CommunityComments, CommunityCommentBaseDto>.MapToDto(comments);

            return commentDtos;
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
    }
}
