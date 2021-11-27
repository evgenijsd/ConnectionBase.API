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
    public class CrossService<T, Tdto> : GenericService<T, Tdto>, ICrossService<T, Tdto>  where T : class where Tdto : class
    {
        private readonly IGenericRepository<Cross> _crosses;
        private readonly IGenericRepository<Pair> _pairs;

        public IGenericRepository<Cross> Crosses { get => _crosses; }
        public IGenericRepository<Pair> Pairs { get => _pairs; }

        public CrossService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            _crosses = _unitOfWork.GetRepository<Cross>();
            _pairs = _unitOfWork.GetRepository<Pair>();
        }

        public override async Task<T> AddAsync(Tdto data)
        {
            var t = await base.AddAsync(data);
            await AddPairsOfCross((t as Cross).CrossId, (t as Cross).NumberPair);
            return t;
        }

        public override async Task<int> DeleteAsync(int id)
        {
            await DeletePairsOfCross(await Pairs.FindAsync(x => x.Cross == id));
            return await base.DeleteAsync(id);
        }

        public override async Task<T> UpdateAsync(Tdto data, int id)
        {
            var t = await base.UpdateAsync(data, id);
            int numberPair = (await Pairs.FindAsync(x => x.Cross == id)).Count();
            if ((t as Cross).NumberPair > numberPair)
            {
                await AddPairsOfCross((t as Cross).CrossId, (t as Cross).NumberPair, numberPair);
            }
            else if ((t as Cross).NumberPair < numberPair)
            {
                await DeletePairsOfCross(await Pairs.FindAsync(x => x.Cross == id && x.PairNum >= (t as Cross).NumberPair));
            }
            return t;
        }


        private async Task DeletePairsOfCross(List<Pair> pairs)
        {
            foreach (var pair in pairs)
            {
                var pairsInNull = await Pairs.FindAsync(x => x.PairIn == pair.PairId);
                if (pairsInNull != null)
                    foreach (var pairInNull in pairsInNull) pairInNull.PairIn = null;
            }
            Pairs.RemoveRange(pairs);
            await _unitOfWork.CompleteAsync();
        }

        private async Task AddPairsOfCross(int crossId, int numberPair, int startPair = 0)
        {
            if (numberPair > startPair)
            {
                for (int i = startPair; i < numberPair; i++)
                {
                    Pairs.Add(new() { PairNum = i, Cross = crossId });
                    await _unitOfWork.CompleteAsync();
                }
            }
        }
    }
}
