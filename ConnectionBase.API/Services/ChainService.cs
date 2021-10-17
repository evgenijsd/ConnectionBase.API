using AutoMapper;
using ConnectionBase.API.Services.Interface;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectionBase.API.Services
{
    public class ChainService : IChainService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ChainService(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PairDto>> GetAllChain()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, PairDto>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Pair>, List<PairDto>>(await _unitOfWork.Pairs.GetAllAsync());
        }
    }

}
