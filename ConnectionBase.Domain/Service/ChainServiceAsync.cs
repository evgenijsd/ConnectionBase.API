using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Service
{

    public class ChainServiceAsync<Tlist, Tchain>: IChainServiceAsync<Tlist, Tchain> where Tlist : class where Tchain : class
    {
        private const int ChainNumStart = 0;
        IUnitOfWorkAsync _unitOfWork;

        public ChainServiceAsync(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Pair>> FindChainEndsAsync()
        {
            var pairListIn = await _unitOfWork.GetRepositoryAsync<Pair>().FindAsync(x => x.PairIn != null);
            List<Pair> pairEndChainsList = new();
            foreach (Pair pair in pairListIn)
            {
                if (await _unitOfWork.GetRepositoryAsync<Pair>().AnyAsync(x => x.PairIn == pair.PairId) == null)
                    pairEndChainsList.Add(pair);
            }
            return pairEndChainsList;
        }

        public async Task FindChainAsync(Pair pair, List<GenerationChains> Chains, int pairEnd, int numberInChain)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, GenerationChains>());
            var mapper = new Mapper(config);
            var pairChain = mapper.Map<Pair, GenerationChains>(pair);
            {
                pairChain.PairEnd = pairEnd;
                pairChain.NumChain = numberInChain;
                if (pair.Cross == null)
                {
                    var device = await _unitOfWork.GetRepositoryAsync<Device>().AnyAsync(x => x.Pair == pair.PairId);
                    if (device !=  null)
                    {
                        pairChain.Device = device.DeviceId;
                        if (device.Room != null)
                        {
                            pairChain.Room = device.Room;
                            pairChain.Building = device.RoomNavigation.Building;
                        }
                    }
                    
                }
                else
                {
                    pairChain.Room = pair.CrossNavigation?.Room;
                    pairChain.Building = pair.CrossNavigation?.RoomNavigation?.Building;
                }
            }
            Chains.Add(pairChain);
            numberInChain++;
            var pairtmp = await _unitOfWork.GetRepositoryAsync<Pair>().AnyAsync(x => x.PairId == pair.PairIn);
            if (pair.PairIn != null) await FindChainAsync(pairtmp, Chains, pairEnd, numberInChain);
        }


        public async Task<List<Tchain>> GetChainAsync(int pairEndId)
        {
            var pairEnd = await _unitOfWork.GetRepositoryAsync<Pair>().GetByIdAsync(pairEndId);
            List<GenerationChains> Chain = new();
            await FindChainAsync(pairEnd, Chain, pairEnd.PairId, ChainNumStart);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<GenerationChains, Tchain>());
            var mapper = new Mapper(config);
            return mapper.Map<List<GenerationChains>, List<Tchain>>(Chain);
        }

        public async Task<List<Tchain>> GetAllChainAsync()
        {
            List<Pair> pairEndChainsList = await FindChainEndsAsync();
            List<GenerationChains> Chains = new();
            foreach (Pair pairEnd in pairEndChainsList)
            {
                await FindChainAsync(pairEnd, Chains, pairEnd.PairId, ChainNumStart);
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<GenerationChains, Tchain>());
            var mapper = new Mapper(config);
            return mapper.Map<List<GenerationChains>, List<Tchain>>(Chains);
        }

        public async Task<List<Tlist>> GetListChainsAsync()
        {
            List<Pair> pairEndChainsList = await FindChainEndsAsync();
            List<GenerationList> listChains = new();
            foreach (Pair pairEnd in pairEndChainsList)
            {
                GenerationList pairList = new();
                List<GenerationChains> Chain = new();
                await FindChainAsync(pairEnd, Chain, pairEnd.PairId, ChainNumStart);
                pairList.PairEnd = Chain[0].PairId;
                pairList.PairNumEnd = Chain[0].PairNum;
                pairList.CrossEnd = Chain[0].Cross;
                pairList.DeviceEnd = Chain[0].Device;
                pairList.RoomEnd = Chain[0].Room;
                pairList.BuildingEnd = Chain[0].Building;
                pairList.PairBegin = Chain.Last().PairId;
                pairList.PairNumBegin = Chain.Last().PairNum;
                pairList.CrossBegin = Chain.Last().Cross;
                pairList.DeviceBegin = Chain.Last().Device;
                pairList.RoomBegin = Chain.Last().Room;
                pairList.BuildingBegin = Chain.Last().Building;
                listChains.Add(pairList);

            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<GenerationList, Tlist>());
            var mapper = new Mapper(config);
            return mapper.Map<List<GenerationList>, List<Tlist>>(listChains);
        }
    }

}
