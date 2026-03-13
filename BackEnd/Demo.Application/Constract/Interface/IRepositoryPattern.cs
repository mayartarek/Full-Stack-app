using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Constract.Interface
{
    public interface IRepositoryPattern<T> where T : Domain.Entities.BaseEntity
    {
        Task<T> GetByIdDetachedAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<T>> GetAllPagedAsync(int page, int pageSize);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> searchBy);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> searchBy);
    }
}
