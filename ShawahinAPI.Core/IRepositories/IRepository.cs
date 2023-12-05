using ShawahinAPI.Core.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShawahinAPI.Core.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T?>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<ResultDto> AddAsync(T entity);
        Task<ResultDto> UpdateAsync(T entity);
        Task<ResultDto> RemoveAsync(T entity);

        Task<IEnumerable<T?>> GetByConditionAsync(Expression<Func<T, bool>> condition);

    }
}
