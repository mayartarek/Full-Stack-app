using Demo.Application.Constract.Interface;
using Demo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presintance.Repoitory
{
    public class Repository<T>: IRepositoryPattern<T> where T : Domain.Entities.BaseEntity
    {
        protected readonly DemoDbContext _context;
        protected readonly DbSet<T> _dbSet ;
        public Repository(DemoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async virtual Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> searchBy)
        {
            return await _dbSet.Where(searchBy).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllPagedAsync(int page, int pageSize)
        {
            return await _dbSet.Where(a => !a.IsDeleted).Where(a => !a.IsDeleted).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.Where(a => !a.IsDeleted).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T> GetByIdDetachedAsync(Guid id)
        {
            var entity = await _dbSet.Where(a => !a.IsDeleted).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<int> GetTotalCountAsync()
        {
            return await _dbSet.Where(a => !a.IsDeleted).CountAsync();
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> searchBy)
        {
            return await _dbSet.Where(a => !a.IsDeleted).Where(searchBy).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            // _context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
