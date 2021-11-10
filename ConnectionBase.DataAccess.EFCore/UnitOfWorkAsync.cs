using ConnectionBase.DataAccess.EFCore.Repositories;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConnectionBase.DataAccess.EFCore
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private readonly ConnectionBaseContext _context;
        private Dictionary<Type, object> _repositoriesAsync;
        public ConnectionBaseContext Context { get => _context; }

        public UnitOfWorkAsync(ConnectionBaseContext context)
        {
            _context = context;
        }

        public IGenericRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositoriesAsync == null) _repositoriesAsync = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositoriesAsync.ContainsKey(type)) _repositoriesAsync[type] = new GenericRepositoryAsync<TEntity>(Context);
            return (IGenericRepositoryAsync<TEntity>)_repositoriesAsync[type];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
