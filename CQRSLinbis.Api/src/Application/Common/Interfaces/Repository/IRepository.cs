using CQRSLinbis.Application.Common.Base;
using CQRSLinbis.Application.Common.Models;
using System.Linq.Expressions;

namespace CQRSLinbis.Application.Common.Interfaces.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<PaginatedList<TResult>> GetPaginatedListAsync<TResult>(
                   Expression<Func<T, bool>> filter,
                   Expression<Func<T, TResult>> selector,
                   Expression<Func<T, object>> include,
                   PagerBase pager)
                   where TResult : class;
    }
}
