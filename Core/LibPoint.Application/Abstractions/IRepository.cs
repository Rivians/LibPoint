using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Abstractions
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = false, params Expression<Func<T, object>>[] includeEntity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = false, params Expression<Func<T, object>>[] includeEntity);
        Task<T> GetByIdAsync(Guid id, bool tracking = true);
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(List<T> entites);
        bool Update(T entity);
        bool Delete(T entity);
        Task<bool> SaveChangesAsync();
        Task<bool> ExecuteTransactionAsync(Func<Task<bool>> operation);
    }
}
