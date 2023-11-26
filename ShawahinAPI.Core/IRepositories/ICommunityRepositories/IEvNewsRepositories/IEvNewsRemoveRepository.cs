using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.CummunityEntities;


namespace ShawahinAPI.Core.IRepositories.ICommunityRepositories.IEvNewsRepositories
{
    /// <summary>
    /// Repository for removing EV News.
    /// </summary>
    public interface IEvNewsRemoveRepository
    {
        /// <summary>
        /// Remove an EV News item.
        /// </summary>
        /// <param name="news">The EV News to remove.</param>
        /// <returns>A result indicating the success of the operation.</returns>
        Task<ResultDto> RemoveNewsAsync(CommunityEvNews news);
    }
}
