using ConnectionBase.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.Logic
{
    public class ResultChain
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public ResultChain(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
