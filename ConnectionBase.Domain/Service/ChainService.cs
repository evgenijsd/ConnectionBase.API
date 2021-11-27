using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Service
{

    public class ChainService<Tlist, Tchain>: IChainService<Tlist, Tchain> where Tlist : class where Tchain : class
    {
        private const int CHAIN_NUM_START = 0;
        private IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Device> _devices;
        private readonly IGenericRepository<Cross> _crosses;
        private readonly IGenericRepository<Pair> _pairs;

        public IGenericRepository<Device> Devices { get => _devices; }
        public IGenericRepository<Cross> Crosses { get => _crosses; }
        public IGenericRepository<Pair> Pairs { get => _pairs; }

        public ChainService(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            _devices = _unitOfWork.GetRepository<Device>();
            _crosses = _unitOfWork.GetRepository<Cross>();
            _pairs = _unitOfWork.GetRepository<Pair>();
        }

        public async Task<List<Pair>> FindChainEndsAsync()
        {
            var pairListIn = await Pairs.FindAsync(x => x.PairIn != null);
            List<Pair> pairEndChainsList = new();
            foreach (Pair pair in pairListIn)
            {
                if (await Pairs.AnyAsync(x => x.PairIn == pair.PairId) == null)
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
                    var device = await Devices.GetAsync(
                                                    expression: x => x.Pair == pair.PairId,
                                                    include: x => x
                                                    .Include(x => x.RoomNavigation)
                                                    .ThenInclude(x => x.BuildingNavigation));
                    if (device !=  null)
                    {
                        pairChain.Device = device.DeviceId;
                        if (device.Room != null)
                        {
                            var room = device.RoomNavigation;
                            pairChain.Room = room.RoomId;
                            var building = room.BuildingNavigation;
                            pairChain.Building = building.BuildingId;
                        }
                    }
                    
                }
                else
                {
                    var cross = await Crosses.GetAsync(
                                                    expression: x => x.CrossId == pair.Cross,
                                                    include: x => x
                                                    .Include(x => x.RoomNavigation)
                                                    .ThenInclude(x => x.BuildingNavigation));
                    var room = cross.RoomNavigation;
                    pairChain.Room = room.RoomId;
                    var building = room.BuildingNavigation;
                    pairChain.Building = building.BuildingId;
                }
            }
            Chains.Add(pairChain);
            numberInChain++;
            var pairtmp = await Pairs.AnyAsync(x => x.PairId == pair.PairIn);
            if (pair.PairIn != null) await FindChainAsync(pairtmp, Chains, pairEnd, numberInChain);
        }


        public async Task<List<Tchain>> GetChainAsync(int pairEndId)
        {
            var pairEnd = await Pairs.GetByIdAsync(pairEndId);
            List<GenerationChains> Chain = new();
            if (pairEnd != null) await FindChainAsync(pairEnd, Chain, pairEnd.PairId, CHAIN_NUM_START);

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
                await FindChainAsync(pairEnd, Chains, pairEnd.PairId, CHAIN_NUM_START);
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
                await FindChainAsync(pairEnd, Chain, pairEnd.PairId, CHAIN_NUM_START);
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
