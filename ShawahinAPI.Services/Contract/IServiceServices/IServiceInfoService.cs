
using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;

namespace ShawahinAPI.Services.Contract.IServiceServices
{
    /// <summary>
    /// Service contract for managing Service Information.
    /// </summary>
    public interface IServiceInfoService
    {
        /// <summary>
        /// Gets all service information asynchronously.
        /// </summary>
        /// <returns>A collection of service information response DTOs.</returns>
        Task<IEnumerable<ServiceInfoResponseDto>> GetAllServiceInfosAsync();

        /// <summary>
        /// Gets service information by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the service information.</param>
        /// <returns>The service information response DTO.</returns>
        Task<ServiceInfoResponseDto> GetServiceInfoByIdAsync(Guid id);

        /// <summary>
        /// Adds a new service information asynchronously.
        /// </summary>
        /// <param name="serviceInfoDto">The service information base DTO.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> AddServiceInfoAsync(ServiceInfoBaseDto serviceInfoDto);

        /// <summary>
        /// Updates an existing service information asynchronously.
        /// </summary>
        /// <param name="serviceInfoDto">The service information response DTO.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> UpdateServiceInfoAsync(ServiceInfoResponseDto serviceInfoDto);

        /// <summary>
        /// Removes service information by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the service information to remove.</param>
        /// <returns>The result of the operation.</returns>
        Task<ResultDto> RemoveServiceInfoAsync(Guid id);
    }
}
