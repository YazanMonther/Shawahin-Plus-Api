using ShawahinAPI.Core.DTO.CommunityDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.CummunityEntities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract.ICommunityServices;


namespace ShawahinAPI.Services.Implementation.CommunityServices
{
    public class EvNewsService : IEvNewsService
    {
        private readonly IRepository<CommunityEvNews> _repository;

        public EvNewsService(IRepository<CommunityEvNews> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ResultDto> AddNewsAsync(CommunityEvNewsBaseDto? newsDto)
        {
            if (newsDto == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid news Provided" };
            }
            var news = EntityDtoMapper<CommunityEvNews, CommunityEvNewsBaseDto>.MapToEntity(newsDto);

            if (news == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid news Provided" };
            }

            return await _repository.AddAsync(news);
        }

        public async Task<IEnumerable<CommunityEvNewsResponseDto?>> GetAllNewsAsync()
        {
            var evNews = await _repository.GetAllAsync();

            if(evNews == null)
            {
                return Enumerable.Empty<CommunityEvNewsResponseDto>();
            }

            // Map entities to DTOs as needed
            var evNewsDtos = EntityDtoMapper<CommunityEvNews, CommunityEvNewsResponseDto>.MapToDto(evNews);

            return evNewsDtos.Where(dto => dto != null);
        }

        public async Task<CommunityEvNewsResponseDto?> GetNewsByIdAsync(Guid newsId)
        {
            var evNews = await _repository.GetByIdAsync(newsId);

            // Map entity to DTO as needed
            var evNewsDto = EntityDtoMapper<CommunityEvNews, CommunityEvNewsResponseDto>.MapToDto(evNews);

            return evNewsDto;
        }

        public async Task<ResultDto> RemoveNewsAsync(Guid newsId)
        {
            var existingNews = await _repository.GetByIdAsync(newsId);

            if (existingNews == null)
            {
                return new ResultDto { Succeeded = false, Message = "EV News not found." };
            }

            return await _repository.RemoveAsync(existingNews);
        }

        public async Task<ResultDto> UpdateNewsAsync(CommunityEvNewsBaseDto? newsDto, Guid newsId)
        {
            if(newsDto == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invlaid EV News ." };

            }
            var existingNews = await _repository.GetByIdAsync(newsId);

            if (existingNews == null)
            {
                return new ResultDto { Succeeded = false, Message = "EV News not found." };
            }

            // Update properties of existing news with values from the DTO
            EntityDtoMapper<CommunityEvNews, CommunityEvNewsBaseDto>.MapToEntity(newsDto, existingNews);

            return await _repository.UpdateAsync(existingNews);
        }
    }
}
