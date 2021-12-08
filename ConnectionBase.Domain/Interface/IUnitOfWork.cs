﻿using ConnectionBase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Interface
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync();
    }
}
