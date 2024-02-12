using CQRSLinbis.Application.Common.Base;
using CQRSLinbis.Application.Common.Interfaces.Repository;
using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Domain.Entities;
using CQRSLinbis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRSLinbis.Infrastructure.Repository
{
    public class DatabaseRepository<T> : IRepository<T> where T: class, IEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public DatabaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(int id, Expression<Func<T, object>> include)
        {
            var query = _dbSet.AsQueryable();

            if (include != null)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<PaginatedList<TResult>> GetPaginatedListAsync<TResult>(
                   Expression<Func<T, bool>> filter,
                   Expression<Func<T, TResult>> selector,
                   Expression<Func<T, object>> include,
                   PagerBase pager)
                   where TResult : class
        {
            var query = _dbSet.AsQueryable();

            if (include != null) query = query.Include(include);

            if (filter != null) query = query.Where(filter);

            var totalCount = await query.CountAsync();

            if (pager != null)
                query = query.Skip((pager.PageNumber - 1) * pager.PageSize)
                             .Take(pager.PageSize);

            var items = await query.Select(selector).ToListAsync();

            return new PaginatedList<TResult>(items, totalCount, pager?.PageNumber ?? 1, pager?.PageSize ?? totalCount);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
