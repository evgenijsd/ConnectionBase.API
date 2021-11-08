using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class CrossController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public CrossController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task DeletePairsOfCross(List<Pair> pairs)
        {
            foreach (var pair in pairs)
            {
                var pairsInNull = await _unitOfWork.Pairs.FindAsync(x => x.PairIn == pair.PairId);
                if (pairsInNull != null) 
                    foreach (var pairInNull in pairsInNull) pairInNull.PairIn = null;
            }
            //_unitOfWork.Pairs.RemoveRange(pairs); не нужен - каскадное удаление
        }

        private async Task AddPairsOfCross(int crossId, int numberPair, int startPair = 0)
        {
            if (numberPair > startPair)
            {
                for (int i = startPair; i < numberPair; i++)
                {
                    _unitOfWork.Pairs.Add(new() { PairNum = i, Cross = crossId });
                    await _unitOfWork.CompleteAsync();
                }
            }
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetCrossesAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cross, CrossDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<Cross>, List<CrossDto>>(await _unitOfWork.Crosses.GetAllAsync()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetCrossAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cross, CrossDto>());
            var mapper = new Mapper(config);

            var cross = mapper.Map<Cross, CrossDto>(await _unitOfWork.Crosses.GetByIdAsync(id));
            if (cross == null)
                return NotFound();
            return Ok(cross);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddCrossAsync(CrossDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CrossDto, Cross>());
            var mapper = new Mapper(config);
            Cross cross = mapper.Map<CrossDto, Cross>(data);

            _unitOfWork.Crosses.Add(cross);
            await _unitOfWork.CompleteAsync();
            await AddPairsOfCross(cross.CrossId, data.NumberPair);
            config = new MapperConfiguration(cfg => cfg.CreateMap<Cross, CrossDto>());
            mapper = new Mapper(config);
            data = mapper.Map<Cross, CrossDto>(cross);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateCrossAsync(CrossDto data)
        {
            if (data == null)
                return BadRequest();
            Cross cross = await _unitOfWork.Crosses.GetByIdAsync(data.CrossId);
            if (cross == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CrossDto, Cross>());
            var mapper = new Mapper(config);
            cross = mapper.Map<CrossDto, Cross>(data, cross);*/

            //_unitOfWork.Crosses.Update(cross);
           /* int numberPair = (await _unitOfWork.Pairs.FindAsync(x => x.Cross == data.CrossId)).Count();
            if (data.NumberPair > numberPair) {
                await AddPairsOfCross(cross.CrossId, data.NumberPair, numberPair);
            }
            else if (data.NumberPair < numberPair) {
                await DeletePairsOfCross(await _unitOfWork.Pairs.FindAsync(x => x.Cross == data.CrossId && x.PairNum >= data.NumberPair));
            }
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteCrossAsync(int id)
        {
            Cross cross = await _unitOfWork.Crosses.GetByIdAsync(id);
            if (cross == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cross, CrossDto>());
            var mapper = new Mapper(config);
            var _cross = mapper.Map<Cross, CrossDto>(cross);

            _unitOfWork.Crosses.Remove(cross);
            await DeletePairsOfCross(await _unitOfWork.Pairs.FindAsync(x => x.Cross == id));
            await _unitOfWork.CompleteAsync();
            return Ok(_cross);
        }
    }*/
}
