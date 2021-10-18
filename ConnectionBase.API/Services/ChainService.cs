using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.API.Services.Interface;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using System.Collections.Generic;
using System.Linq;
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

        public void FindChain(ref List<GenerationChains> Chains, ref int numberInChain, in int pairEnd, in List<Pair> list, Pair pair)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, GenerationChains>());
            var mapper = new Mapper(config);
            var pairChain = mapper.Map<Pair, GenerationChains>(pair);
            pairChain.PairEnd = pairEnd;
            pairChain.NumChain = numberInChain;

            Chains.Add(pairChain);
            numberInChain++;
            if (pair.PairIn != null)
            {
                var pairtmp = list.FirstOrDefault(x => x.PairId == pair.PairIn);
                FindChain(ref Chains, ref numberInChain, in pairEnd, in list, pairtmp);
            }
        }

        public async Task<List<GenerationChains>> CreateChains()
        {
            var pairList = (List <Pair>)await _unitOfWork.Pairs.GetAllAsync();
            List<Pair> pairEndChainsList = new();
            List<GenerationChains> Chains = new();
            foreach (Pair pair in pairList)
            {
                if (pair.PairIn != null)
                {
                    var pairtmp = pairList.FirstOrDefault(x => x.PairIn == pair.PairId);
                    if (pairtmp == null) pairEndChainsList.Add(pair);
                }
            }
            foreach (Pair pair in pairEndChainsList)
            {
                int numberInChain = 0;
                FindChain(ref Chains, ref numberInChain, pair.PairId, in pairList, pair);
            }
            return Chains;
        }

        public async Task<List<GenerationChains>> GetAllChain()
        {
            return await CreateChains();
        }

        public async Task<List<GenerationList>> GetListChains()
        {
            List<GenerationList> listChains = new();
            List<GenerationChains> Chains = await CreateChains();
            GenerationList pairList = new();
            foreach (GenerationChains pair in Chains)
            {
                if (pair.NumChain == 0)
                {
                    pairList.PairEnd = pair.PairId;
                    pairList.PairNumEnd = pair.PairNum;
                    pairList.CrossEnd = pair.Cross;
                    listChains.Add(pairList);
                }
                else
                {
                    listChains.Last().PairBegin = pair.PairId;
                    listChains.Last().PairNumBegin = pair.PairNum;
                    listChains.Last().CrossBegin = pair.Cross;
                }
            }
            return listChains;
        }
    }

}
