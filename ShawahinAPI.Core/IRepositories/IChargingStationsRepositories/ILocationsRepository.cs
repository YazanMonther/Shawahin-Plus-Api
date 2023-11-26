using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{
    /// <summary>
    /// Repository interface for managing <see cref="Locations"/> entities.
    /// </summary>
    public interface ILocationsRepository
    {
        /// <summary>
        /// Asynchronously adds a new <see cref="Locations"/> entity.
        /// </summary>
        /// <param name="location">The location to add.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> AddAsync(Locations? location);

        /// <summary>
        /// Asynchronously retrieves all <see cref="Locations"/> entities.
        /// </summary>
        /// <returns>A collection of all locations.</returns>
        Task<IEnumerable<Locations?>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves a <see cref="Locations"/> entity by ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the location with the specified ID.</returns>
        Task<Locations?> GetByIdAsync(Guid? id);

        /// <summary>
        /// Asynchronously removes a <see cref="Locations"/> entity.
        /// </summary>
        /// <param name="location">The location to remove.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> RemoveAsync(Locations? location);

        /// <summary>
        /// Asynchronously updates a <see cref="Locations"/> entity.
        /// </summary>
        /// <param name="location">The location to update.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> UpdateAsync(Locations? location);
    }

}
