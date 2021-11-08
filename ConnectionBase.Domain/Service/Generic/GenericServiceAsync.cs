using AutoMapper;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Service.Generic
{
    public class GenericServiceAsync<T, Tdto> : IGenericServiceAsync<T, Tdto> where T : class where Tdto : class
    {
        protected IUnitOfWorkAsync _unitOfWork;

        public GenericServiceAsync(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<int> AddAsync(Tdto data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Tdto, T>());
            var mapper = new Mapper(config);
            T t = mapper.Map<Tdto, T>(data);

            _unitOfWork.GetRepositoryAsync<T>().Add(t);
            return await _unitOfWork.CompleteAsync();
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            T t = await _unitOfWork.GetRepositoryAsync<T>().GetByIdAsync(id);

            _unitOfWork.GetRepositoryAsync<T>().Remove(t);
            return await _unitOfWork.CompleteAsync();
        }

        public virtual async Task<List<Tdto>> GetAsync(Expression<Func<T, bool>> expression)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, Tdto>());
            var mapper = new Mapper(config);

            return mapper.Map<List<T>, List<Tdto>>(await _unitOfWork.GetRepositoryAsync<T>().FindAsync(expression));
        }

        public virtual async Task<Tdto> GetOneAsync(Expression<Func<T, bool>> expression)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, Tdto>());
            var mapper = new Mapper(config);

            return mapper.Map<T, Tdto>(await _unitOfWork.GetRepositoryAsync<T>().AnyAsync(expression));
        }

        public virtual async Task<List<Tdto>> GetAllAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, Tdto>());
            var mapper = new Mapper(config);
            return mapper.Map<List<T>, List<Tdto>>(await _unitOfWork.GetRepositoryAsync<T>().GetAllAsync());
        }

        public virtual async Task<Tdto> GetByIdAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, Tdto>());
            var mapper = new Mapper(config);
            return mapper.Map<T, Tdto>(await _unitOfWork.GetRepositoryAsync<T>().GetByIdAsync(id));
        }

        public async Task<int> UpadateAsync(Tdto data, int id)
        {
            T t = await _unitOfWork.GetRepositoryAsync<T>().GetByIdAsync(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Tdto, T>());
            var mapper = new Mapper(config);
            t = mapper.Map<Tdto, T>(data, t);

            //_unitOfWork.Buildings.Update(building);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
