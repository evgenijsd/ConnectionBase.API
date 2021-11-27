using AutoMapper;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Generic;
using ConnectionBase.Domain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.Domain.Service
{
    public class DeviceService<T, Tdto> : GenericService<T, Tdto>, IDeviceService<T, Tdto>  where T : class where Tdto : class
    {
        private readonly new IGenericRepository<T> _repository;
        private readonly IGenericRepository<Pair> _pairs;
        public IGenericRepository<Pair> Pairs { get => _pairs; }

        public DeviceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            _pairs = _unitOfWork.GetRepository<Pair>();
            _repository = _unitOfWork.GetRepository<T>();
        }

        public override async Task<T> AddAsync(Tdto data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Tdto, T>());
            var mapper = new Mapper(config);
            T t = mapper.Map<Tdto, T>(data);
            (t as Device).Pair = await AddPairOfDevice();
            config = new MapperConfiguration(cfg => cfg.CreateMap<T, Tdto>());
            mapper = new Mapper(config);
            data = mapper.Map<T, Tdto>(t);

            t = await base.AddAsync(data);
            return t;
        }

        public override async Task<int> DeleteAsync(int id)
        {
            T t = await _repository.GetByIdAsync(id);
            if (t != null)
            {
                await DeletePairOfDevice(await Pairs.GetByIdAsync((int)(t as Device).Pair));
                _repository.Remove(t);
            }
            return await _unitOfWork.CompleteAsync();
        }

        private async Task DeletePairOfDevice(Pair pair)
        {
            var pairsInNull = await Pairs.FindAsync(x => x.PairIn == pair.PairId);
            if (pairsInNull != null)
                foreach (var pairInNull in pairsInNull) pairInNull.PairIn = null;

            Pairs.Remove(pair);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<int> AddPairOfDevice()
        {
            Pair pair = new() { PairNum = 0, Cross = null };
            Pairs.Add(pair);
            await _unitOfWork.CompleteAsync();
            return pair.PairId;
        }
    }
}
