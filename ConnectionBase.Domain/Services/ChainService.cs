using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Services
{
    public class ChainService : IChainService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ChainService(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Pair>> GetAllChain() => await _unitOfWork.Pairs.GetAllAsync();
    }
}
