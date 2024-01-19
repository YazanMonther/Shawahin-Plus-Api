using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
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

        //Eager Loading

        Task<IEnumerable<T?>> GetByConditionAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T?>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
    }
}
