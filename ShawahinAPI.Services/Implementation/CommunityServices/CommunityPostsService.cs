using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract.ICommunityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Implementation.CommunityServices
{
    public class CommunityPostsService : ICommunityPostsService
    {
        private readonly IRepository<CommunityPosts> _repository;
        private readonly IRepository<CommunityComments> _comRepository;
        private readonly IUserGetRepository _userRepository;

        public CommunityPostsService(IUserGetRepository userRepository, IRepository<CommunityComments> comRepository,IRepository<CommunityPosts> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _comRepository = comRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultDto> AddPostAsync(CommunityPostBaseDto? postDto)
        {
            var communityPost = EntityDtoMapper<CommunityPosts, CommunityPostBaseDto>.MapToEntity(postDto);

            if (communityPost == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid post provider" };
            }

            return await _repository.AddAsync(communityPost);
        }

        public async Task<IEnumerable<CommunityPostResponseDto?>> GetAllPostsAsync()
        {
            var posts = await _repository.GetAllAsync();

            // Map entities to DTOs as needed
            var postDtos = EntityDtoMapper<CommunityPosts, CommunityPostResponseDto>.MapToDto(posts).ToList();

            var updatedPostDtos = postDtos.Select( postDto =>
            {
                var comments =  _comRepository.GetByConditionAsync(c => c.PostId == postDto.Id).Result;
                var user = _userRepository.GetUserByIdAsync(postDto.UserId).Result;

                // Update the Comments property
                postDto.NumberOfComments = comments.Count();
                postDto.UserName = user.Fname + " " + user.Lname;

                return postDto;
            });

            return updatedPostDtos;
        }

        public async Task<CommunityPostResponseDto?> GetPostByIdAsync(Guid postId)
        {
            var communityPost = await _repository.GetByIdAsync(postId);

            if(communityPost == null)
                return null;
            // Map entity to DTO as needed
            var postDto = EntityDtoMapper<CommunityPosts, CommunityPostResponseDto>.MapToDto(communityPost);

            return postDto;
        }

        public async Task<IEnumerable<CommunityPostResponseDto?>> GetPostsByUserIdAsync(Guid userId)
        {
            var posts = await _repository.GetByConditionAsync(p => p.UserId == userId);

            if (posts == null)
                return Enumerable.Empty<CommunityPostResponseDto>();

            // Map entities to DTOs as needed
            var postDtos = EntityDtoMapper<CommunityPosts, CommunityPostResponseDto>.MapToDto(posts);
                return postDtos;
        }


        public async Task<ResultDto> RemovePostAsync(Guid postId)
        {
            var communityPost = await _repository.GetByIdAsync(postId);

            if (communityPost == null)
            {
                return new ResultDto { Succeeded = false, Message = "Community Post not found" };
            }

            return await _repository.RemoveAsync(communityPost);
        }

        public async Task<ResultDto> UpdatePostAsync(CommunityPostBaseDto? postDto, Guid postId)
        {
            // Check if the provided DTO is null
            if (postDto == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid post provider" };
            }

            // Retrieve the existing community post from the repository
            var existingPost = await _repository.GetByIdAsync(postId);

            // Check if the existing post is not found
            if (existingPost == null)
            {
                return new ResultDto { Succeeded = false, Message = "Community Post not found" };
            }

            // Update the properties of the existing post with values from the DTO
            EntityDtoMapper<CommunityPosts, CommunityPostBaseDto >.MapToEntity(postDto, existingPost);

            // Call the repository to update the existing post
            return await _repository.UpdateAsync(existingPost);
        }
    }
}
