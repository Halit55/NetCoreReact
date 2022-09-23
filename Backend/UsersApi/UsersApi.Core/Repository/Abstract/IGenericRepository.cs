using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Users.Core.Repository.Abstract
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<Boolean> AddRangeAsync(IEnumerable<T> entity);
        Task<Boolean> DeleteRangeAsync(IEnumerable<T> entity);
        Task<Boolean> UpdateRangeAsync(IEnumerable<T> entity);
        Task<T> DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
