using LibPoint.Application.Abstractions;
using LibPoint.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Persistence.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibPointDbContext _context;
        public Repository(LibPointDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<T> entites)
        {
            await Table.AddRangeAsync(entites);
        }

        public bool Delete(T entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            if (includeEntity.Any())
                foreach (var entity in includeEntity)
                    query = query.Include(entity);

            if (expression != null)
                query = query.Where(expression);

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            if (includeEntity.Any())
                foreach (var entity in includeEntity)
                    query = query.Include(entity);

            if (expression is not null)
                query = query.Where(expression);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id, bool tracking = false)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await Table.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Update(T entity)
        {
            EntityEntry entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}