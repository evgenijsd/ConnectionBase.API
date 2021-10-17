﻿using ConnectionBase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Interface
{
    public interface IChainService
    {
        Task<IEnumerable<Pair>> GetAllChain();
    }
}
