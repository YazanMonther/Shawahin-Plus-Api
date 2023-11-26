using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;

namespace ShawahinAPI.Core.IRepositories.IChargingStationsRepositories
{
    /// <summary>
    /// Repository for managing contacts.
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Asynchronously adds a new contact.
        /// </summary>
        /// <param name="contact">The contact to add.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> AddContactAsync(Contacts contact);

        /// <summary>
        /// Asynchronously retrieves a contact by ID.
        /// </summary>
        /// <param name="contactId">The ID of the contact to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the contact with the specified ID.</returns>
        Task<Contacts?> GetContactByIdAsync(Guid contactId);

        /// <summary>
        /// Asynchronously retrieves all contacts.
        /// </summary>
        /// <returns>A collection of all contacts.</returns>
        Task<IEnumerable<Contacts?>> GetAllContactsAsync();

        /// <summary>
        /// Asynchronously removes a contact.
        /// </summary>
        /// <param name="contact">The contact to remove.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> RemoveContactAsync(Contacts? contact);

        /// <summary>
        /// Asynchronously updates an existing contact.
        /// </summary>
        /// <param name="contact">The contact to update.</param>
        /// <returns>A task representing the asynchronous operation with a ResultDto indicating the result of the operation.</returns>
        Task<ResultDto?> UpdateContactAsync(Contacts? contact);

        /// <summary>
        /// Asynchronously retrieves contacts created by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve contacts for.</param>
        /// <returns>A collection of contacts created by the specified user.</returns>
        Task<IEnumerable<Contacts?>> GetContactsCreatedByUserAsync(Guid? userId);
    }
}