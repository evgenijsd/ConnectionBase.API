using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Service.Interface
{
    public interface IGenericService<T, Tdto>
    {
        public Task<List<Tdto>> GetAllAsync();

        public Task<Tdto> GetByIdAsync(int id);

        public Task<T> AddAsync(Tdto data);

        public Task<T> UpdateAsync(Tdto data, int id);

        public Task<int> DeleteAsync(int id);

        public Task<List<Tdto>> GetAsync(Expression<Func<T, bool>> predicate);
        public Task<Tdto> GetOneAsync(Expression<Func<T, bool>> predicate);
    }

}
