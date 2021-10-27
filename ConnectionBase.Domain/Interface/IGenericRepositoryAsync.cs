using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Interface
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> AnyAsync(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(List<T> entities);
        void Remove(T entity);
        void RemoveRange(List<T> entities);
        void Update(T entity);
    }
}
