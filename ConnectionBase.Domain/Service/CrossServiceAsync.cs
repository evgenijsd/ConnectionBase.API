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
    public class CrossServiceAsync<T, Tdto> : GenericServiceAsync<T, Tdto>, ICrossServiceAsync<T, Tdto>  where T : class where Tdto : class
    {
        public CrossServiceAsync(IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }

        public override async Task<T> AddAsync(Tdto data)
        {
            var t = await base.AddAsync(data);
            await AddPairsOfCross((t as Cross).CrossId, (t as Cross).NumberPair);
            return t;
        }

        public override async Task<int> DeleteAsync(int id)
        {
            await DeletePairsOfCross(await _unitOfWork.GetRepositoryAsync<Pair>().FindAsync(x => x.Cross == id));
            return await base.DeleteAsync(id);
        }

        public override async Task<T> UpdateAsync(Tdto data, int id)
        {
            var t = await base.UpdateAsync(data, id);
            int numberPair = (await _unitOfWork.GetRepositoryAsync<Pair>().FindAsync(x => x.Cross == id)).Count();
            if ((t as Cross).NumberPair > numberPair)
            {
                await AddPairsOfCross((t as Cross).CrossId, (t as Cross).NumberPair, numberPair);
            }
            else if ((t as Cross).NumberPair < numberPair)
            {
                await DeletePairsOfCross(await _unitOfWork.GetRepositoryAsync<Pair>().FindAsync(x => x.Cross == id && x.PairNum >= (t as Cross).NumberPair));
            }
            return t;
        }


        private async Task DeletePairsOfCross(List<Pair> pairs)
        {
            foreach (var pair in pairs)
            {
                var pairsInNull = await _unitOfWork.GetRepositoryAsync<Pair>().FindAsync(x => x.PairIn == pair.PairId);
                if (pairsInNull != null)
                    foreach (var pairInNull in pairsInNull) pairInNull.PairIn = null;
            }
            _unitOfWork.GetRepositoryAsync<Pair>().RemoveRange(pairs);
            await _unitOfWork.CompleteAsync();
        }

        private async Task AddPairsOfCross(int crossId, int numberPair, int startPair = 0)
        {
            if (numberPair > startPair)
            {
                for (int i = startPair; i < numberPair; i++)
                {
                    _unitOfWork.GetRepositoryAsync<Pair>().Add(new() { PairNum = i, Cross = crossId });
                    await _unitOfWork.CompleteAsync();
                }
            }
        }
    }
}
