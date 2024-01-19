using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract.ICommunityServices;


namespace ShawahinAPI.Services.Implementation.CommunityServices
{
    public class CommunityCommentsService : ICommunityCommentsService
    {
        private readonly IRepository<CommunityComments> _repository;
        private readonly IUserGetRepository _userRepository;


        public CommunityCommentsService(IUserGetRepository userRepository,IRepository<CommunityComments> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._userRepository = userRepository;
        }

        public async Task<ResultDto> AddCommentAsync(CommunityCommentBaseDto commentDto)
        {
            var comment = EntityDtoMapper<CommunityComments, CommunityCommentBaseDto>.MapToEntity(commentDto);

            var result = await _repository.AddAsync(comment);

            return result;
        }

        public async Task<IEnumerable<CommunityCommentResponseDto?>> GetAllCommentsAsync()
        {
            var comments = await _repository.GetAllAsync();

            // Map entities to DTOs as needed
            var commentDtos = EntityDtoMapper<CommunityComments, CommunityCommentResponseDto>.MapToDto(comments);
            foreach (var item in commentDtos)
            {
                var user = await _userRepository.GetUserByIdAsync(item.UserId);
                item.name = user?.Fname + " " + user?.Lname;
            }
            return commentDtos;
        }

        public async Task<IEnumerable<CommunityCommentResponseDto?>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _repository.GetByConditionAsync(c => c.PostId == postId);

            // Map entities to DTOs as needed
            var commentDtos = EntityDtoMapper<CommunityComments, CommunityCommentResponseDto>.MapToDto(comments);

            foreach (var item in commentDtos)
            {
                var user = await _userRepository.GetUserByIdAsync(item.UserId);
                item.name = user?.Fname +" "+user?.Lname;
            }
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
