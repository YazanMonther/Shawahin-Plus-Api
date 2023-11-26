using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{
    /// <summary>
    /// Repository interface for managing <see cref="StationOpeningHours"/> entities.
    /// </summary>
    public interface IStationOpeningHoursRepository
    {
        /// <summary>
        /// Asynchronously adds a new <see cref="StationOpeningHours"/> entity.
        /// </summary>
        /// <param name="stationOpeningHours">The <see cref="StationOpeningHours"/> entity to add.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> AddAsync(StationOpeningHours stationOpeningHours);

        /// <summary>
        /// Asynchronously retrieves all <see cref="StationOpeningHours"/> entities.
        /// </summary>
        /// <returns>A collection of all <see cref="StationOpeningHours"/> entities.</returns>
        Task<IEnumerable<StationOpeningHours?>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves a specific <see cref="StationOpeningHours"/> entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="StationOpeningHours"/> entity.</param>
        /// <returns>A task representing the asynchronous operation that returns the retrieved <see cref="StationOpeningHours"/> entity.</returns>
        Task<StationOpeningHours?> GetByIdAsync(Guid id);

        /// <summary>
        /// Asynchronously removes a specific <see cref="StationOpeningHours"/> entity from the repository.
        /// </summary>
        /// <param name="stationOpeningHours">The <see cref="StationOpeningHours"/> entity to remove.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> RemoveAsync(StationOpeningHours stationOpeningHours);

        /// <summary>
        /// Asynchronously updates an existing <see cref="StationOpeningHours"/> entity in the repository.
        /// </summary>
        /// <param name="stationOpeningHours">The <see cref="StationOpeningHours"/> entity to update.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> UpdateAsync(StationOpeningHours stationOpeningHours);
    }

}
