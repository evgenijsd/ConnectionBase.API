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
    public class DeviceServiceAsync<T, Tdto> : GenericServiceAsync<T, Tdto>, IDeviceServiceAsync<T, Tdto>  where T : class where Tdto : class
    {
        public DeviceServiceAsync(IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
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
            T t = await _unitOfWork.GetRepositoryAsync<T>().GetByIdAsync(id);
            if (t != null)
            {
                await DeletePairOfDevice(await _unitOfWork.GetRepositoryAsync<Pair>().GetByIdAsync((int)(t as Device).Pair));
                _unitOfWork.GetRepositoryAsync<T>().Remove(t);
            }
            return await _unitOfWork.CompleteAsync();
        }

        private async Task DeletePairOfDevice(Pair pair)
        {
            var pairsInNull = await _unitOfWork.GetRepositoryAsync<Pair>().FindAsync(x => x.PairIn == pair.PairId);
            if (pairsInNull != null)
                foreach (var pairInNull in pairsInNull) pairInNull.PairIn = null;

            _unitOfWork.GetRepositoryAsync<Pair>().Remove(pair);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<int> AddPairOfDevice()
        {
            Pair pair = new() { PairNum = 0, Cross = null };
            _unitOfWork.GetRepositoryAsync<Pair>().Add(pair);
            await _unitOfWork.CompleteAsync();
            return pair.PairId;
        }
    }
}
