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
    public class DeviceService<T, Tdto> : GenericService<Device, Tdto>, IDeviceService<Device, Tdto>  where Tdto : class
    {
        private readonly IGenericRepository<Pair> _pairs;
        public IGenericRepository<Pair> Pairs { get => _pairs; }

        public DeviceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _pairs = _unitOfWork.GetRepository<Pair>();
        }

        public override async Task<Device> AddAsync(Tdto data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Tdto, Device>());
            var mapper = new Mapper(config);
            Device device = mapper.Map<Tdto, Device>(data);
            device.Pair = await AddPairOfDevice();
            config = new MapperConfiguration(cfg => cfg.CreateMap<Device, Tdto>());
            mapper = new Mapper(config);
            data = mapper.Map<Device, Tdto>(device);

            device = await base.AddAsync(data);
            return device;
        }

        public override async Task<int> DeleteAsync(int id)
        {
            Device device = await _repository.GetByIdAsync(id);
            if (device != null)
            {
                await DeletePairOfDevice(await Pairs.GetByIdAsync((int)device.Pair));
                _repository.Remove(device);
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
