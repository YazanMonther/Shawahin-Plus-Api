using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;


namespace ShawahinAPI.Persistence.Repository.ChargingStationsRepositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ShawahinDbContext _context;

        public ContactRepository(ShawahinDbContext context)
        {
            _context = context;
        }

        #region IContactRepository Implementation

        #region AddContactAsync

        public async Task<ResultDto?> AddContactAsync(Contacts contact)
        {
            if (contact == null)
            {
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = "Invalid contact parameter"
                };
            }

            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = $"Error adding contact: {ex.Message}"
                };
            }
        }

        #endregion

        #region GetContactByIdAsync

        public async Task<Contacts?> GetContactByIdAsync(Guid contactId)
        {
            return await _context.Contacts.FindAsync(contactId);
        }

        #endregion

        #region GetAllContactsAsync

        public async Task<IEnumerable<Contacts?>> GetAllContactsAsync()
        {
            try
            {
                return await _context.Contacts.ToListAsync();
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return new List<Contacts?>();
            }
        }

        #endregion

        #region RemoveContactAsync

        public async Task<ResultDto?> RemoveContactAsync(Contacts? contact)
        {
            if (contact == null)
            {
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = "Invalid contact parameter"
                };
            }

            try
            {
                var result = _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    Succeeded = true,
                    Message = "Contact removed successfully."
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = $"Error removing contact: {ex.Message}"
                };
            }
        }

        #endregion

        #region UpdateContactAsync

        public async Task<ResultDto?> UpdateContactAsync(Contacts? contact)
        {
            if (contact == null)
            {
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = "Invalid contact parameter"
                };
            }

            try
            {
                var result = _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();

                return new ResultDto()
                {
                    Succeeded = true,
                    Message = "Contact updated successfully."
                };
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return new ResultDto()
                {
                    Succeeded = false,
                    Message = $"Error updating contact: {ex.Message}"
                };
            }
        }

        #endregion

        #region GetContactsCreatedByUserAsync

        public async Task<IEnumerable<Contacts?>> GetContactsCreatedByUserAsync(Guid? userId)
        {
            try
            {
                return await _context.Contacts.Where(c => c.Id == userId).ToListAsync();
            }
            catch (Exception)
            {
                // Log or handle the exception as needed
                return new List<Contacts?>();
            }
        }

        #endregion

        #endregion
    }
}
