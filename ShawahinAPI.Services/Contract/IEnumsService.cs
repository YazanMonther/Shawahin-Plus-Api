using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Contract
{
    /// <summary>
    /// Interface for managing payment methods.
    /// </summary>
    public interface IEnumsService
    {
        /// <summary>
        /// Gets all values of the PaymentMethod enum.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of PaymentMethod values.</returns>
        Task<IEnumerable<string>?> GetAllPaymentMethodsAsync();

        /// <summary>
        /// Gets all values of the PaymentType enum.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of PaymentType values.</returns>
        Task<IEnumerable<string>?> GetAllPaymentTypesAsync();

        /// <summary>
        /// Gets all values of the CurrentChargerStatus enum.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of CurrentChargerStatus values.</returns>
        Task<IEnumerable<string>?> GetAllCurrentChargerStatusAsync();

        /// <summary>
        /// Gets all values of the ChargerPower enum.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of ChargerPower values.</returns>
        Task<IEnumerable<string?>?> GetAllChargerPowerAsync();
    }
}
