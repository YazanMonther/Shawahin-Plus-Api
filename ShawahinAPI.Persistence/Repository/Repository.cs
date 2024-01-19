using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.IRepositories;
using System.Linq.Expressions;

namespace ShawahinAPI.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShawahinDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ShawahinDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<ResultDto> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new ResultDto { Succeeded = true, Message = "Entity added successfully." };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = $"Error adding entity: {ex.Message}" };
            }
        }

        public async Task<ResultDto> UpdateAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return new ResultDto { Succeeded = true, Message = "Entity updated successfully." };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = $"Error updating entity: {ex.Message}" };
            }
        }

        public async Task<ResultDto> RemoveAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return new ResultDto { Succeeded = true, Message = "Entity removed successfully." };
            }
            catch (Exception ex)
            {
                return new ResultDto { Succeeded = false, Message = $"Error removing entity: {ex.Message}" };
            }
        }

        public async Task<IEnumerable<T?>> GetByConditionAsync(Expression<Func<T, bool>> condition)
        {
            return await _dbSet.Where(condition).ToListAsync();
        }


        /// Eager Loading
        public async Task<IEnumerable<T?>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T?>> GetByConditionAsync(Expression<Func<T, bool>> condition , params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include).Where(condition);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, "Id") == id);
        }

    }

}
