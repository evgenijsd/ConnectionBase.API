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
    public class PairAbController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public PairAbController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetPairAbsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PairAb, PairAbDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<PairAb>, List<PairAbDto>>(await _unitOfWork.PairAbs.GetAllAsync()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetPairAbAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PairAb, PairAbDto>());
            var mapper = new Mapper(config);

            var pairAb = mapper.Map<PairAb, PairAbDto>(await _unitOfWork.PairAbs.GetByIdAsync(id));
            if (pairAb == null)
                return NotFound();
            return Ok(pairAb);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddPairAbAsync(PairAbDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PairAbDto, PairAb>());
            var mapper = new Mapper(config);
            PairAb pairAb = mapper.Map<PairAbDto, PairAb>(data);

            _unitOfWork.PairAbs.Add(pairAb);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<PairAb, PairAbDto>());
            mapper = new Mapper(config);
            data = mapper.Map<PairAb, PairAbDto>(pairAb);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadatePairAbAsync(PairAbDto data)
        {
            if (data == null)
                return BadRequest();
            PairAb pairAb = await _unitOfWork.PairAbs.GetByIdAsync(data.AbId);
            if (pairAb == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PairAbDto, PairAb>());
            var mapper = new Mapper(config);
            pairAb = mapper.Map<PairAbDto, PairAb>(data, pairAb); ;

            //_unitOfWork.PairAbs.Update(pairAb);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeletePairAbAsync(int id)
        {
            PairAb pairAb = await _unitOfWork.PairAbs.GetByIdAsync(id);
            if (pairAb == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PairAb, PairAbDto>());
            var mapper = new Mapper(config);
            var _pairAb = mapper.Map<PairAb, PairAbDto>(pairAb);

            _unitOfWork.PairAbs.Remove(pairAb);
            await _unitOfWork.CompleteAsync();
            return Ok(_pairAb);
        }
    }*/
}
