using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{
    /// <summary>
    /// Repository interface for operations on <see cref="Chargers"/> entities.
    /// </summary>
    public interface IChargersRepository
    {
        /// <summary>
        /// Adds a new charger asynchronously.
        /// </summary>
        /// <param name="charger">The charger to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto?> AddAsync(Chargers? charger);

        /// <summary>
        /// Gets all chargers asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of chargers.</returns>
        Task<IEnumerable<Chargers>?> GetAllAsync();

        /// <summary>
        /// Gets a charger by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the charger.</param>
        /// <returns>A task representing the asynchronous operation that returns the charger.</returns>
        Task<Chargers?> GetByIdAsync(Guid? id);

        /// <summary>
        /// Removes a charger asynchronously.
        /// </summary>
        /// <param name="charger">The charger to be removed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto?> RemoveAsync(Chargers? charger);

        /// <summary>
        /// Updates an existing charger asynchronously.
        /// </summary>
        /// <param name="charger">The charger to be updated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<ResultDto?> UpdateAsync(Chargers? charger);

        /// <summary>
        /// Get stations with the same power
        /// </summary>
        /// <param name="power">The charger power.</param>
        /// <returns>list of charging statons.</returns>
        Task<IEnumerable<ChargingStations?>?> GetChargingStationsByPower(ChargerPower? power);
    }

}
