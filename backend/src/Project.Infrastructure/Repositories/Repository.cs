using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Application.Interfaces;
using Project.Infrastructure.Data;

namespace Project.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _set;

        public Repository(AppDbContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _set.FindAsync(id) as T;
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<T> AsQueryable()
        {
            return _set.AsQueryable();
        }
    }
}
