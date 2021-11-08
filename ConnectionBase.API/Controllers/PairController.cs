using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class PairController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public PairController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetPairsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, PairDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<Pair>, List<PairDto>>(await _unitOfWork.Pairs.GetAllAsync()));

        }

        [HttpGet("cross/{cross}")]
        [ActionName("cross")]
        public async Task<IActionResult> GetCrossFindAsync(int cross)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, PairDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<Pair>, List<PairDto>>(await _unitOfWork.Pairs.FindAsync(x => x.Cross == cross)));
        }

        [HttpGet("out/{id}")]
        [ActionName("out")]
        public async Task<IActionResult> GetOutFindAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, PairDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<Pair>, List<PairDto>>(await _unitOfWork.Pairs.FindAsync(x => x.PairIn == id)));
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetPairAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, PairDto>());
            var mapper = new Mapper(config);

            var pair = mapper.Map<Pair, PairDto>(await _unitOfWork.Pairs.GetByIdAsync(id));
            if (pair == null)
                return NotFound();
            return Ok(pair);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadatePairAsync(PairDto data)
        {
            if (data == null)
                return BadRequest();
            Pair pair = await _unitOfWork.Pairs.GetByIdAsync(data.PairId);
            if (pair == null ||
               (data.PairIn != null && await _unitOfWork.Pairs.AnyAsync(x => x.PairId == data.PairIn) == null))
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PairDto, Pair>());
            var mapper = new Mapper(config);
            pair = mapper.Map<PairDto, Pair>(data, pair); ;

            //_unitOfWork.Pairs.Update(pair);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }
    }*/
}
