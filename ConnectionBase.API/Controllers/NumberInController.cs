using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberInController : ControllerBase
    {
        private readonly IGenericServiceAsync<NumberIn, NumberInDto> _numberInServiceAsync;
        public NumberInController(IGenericServiceAsync<NumberIn, NumberInDto> numberInServiceAsync)
        {
            _numberInServiceAsync = numberInServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _numberInServiceAsync.GetAllAsync();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest();
            var result = await _numberInServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(NumberInDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _numberInServiceAsync.AddAsync(data);
            var id = result.NumberId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(NumberInDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _numberInServiceAsync.UpdateAsync(data, data.NumberId);
            if (result == null)
                return NotFound();
            return Accepted(data);
        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest();
            var result = await _numberInServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _numberInServiceAsync.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("pair/{pair}")]
        [ActionName("pair")]
        public async Task<IActionResult> GetNumberInPairAsync(int pair)
        {
            if (pair <= 0)
                return BadRequest();
            var result = await _numberInServiceAsync.GetAsync(x => x.PairAts == pair);
            if (result == null)
                return NotFound();
            return Ok(result);

        }

        [HttpGet("num/{num}")]
        [ActionName("num")]
        public async Task<IActionResult> GetNumberInFindAsync(string num)
        {
            if (num == string.Empty)
                return BadRequest();
            var result = await _numberInServiceAsync.GetAsync(x => x.Number_In.Contains(num));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
    /*[Route("api/[controller]")]
    [ApiController]
    public class NumberInController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public NumberInController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetNumberInsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberIn, NumberInDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<NumberIn>, List<NumberInDto>>(await _unitOfWork.NumberIns.GetAllAsync()));
        }

        /*[HttpGet("num/{num}")]
        [ActionName("num")]
        public async Task<IActionResult> GetFindNumberAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberIn, NumberInDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<NumberIn>, List<NumberInDto>>(await _unitOfWork.NumberIns.FindAsync(x => EntryPointNotFoundException.));
        }*/

    /*[HttpGet("pair/{pair}")]
    [ActionName("pair")]
    public async Task<IActionResult> GetNumberInPairAsync(int pair)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberIn, NumberInDto>());
        var mapper = new Mapper(config);
        return Ok(mapper.Map<List<NumberIn>, List<NumberInDto>>(await _unitOfWork.NumberIns.FindAsync(x => x.PairAts == pair)));

    }

    [HttpGet("num/{num}")]
    [ActionName("num")]
    public async Task<IActionResult> GetNumberInFindAsync(string num)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberIn, NumberInDto>());
        var mapper = new Mapper(config);
        return Ok(mapper.Map<List<NumberIn>, List<NumberInDto>>(await _unitOfWork.NumberIns.FindAsync(x => x.Number_In.Contains(num))));
    }

    [HttpGet("{id}")]
    [ActionName("id")]
    public async Task<IActionResult> GetNumberInAsync(int id)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberIn, NumberInDto>());
        var mapper = new Mapper(config);

        var numberIn = mapper.Map<NumberIn, NumberInDto>(await _unitOfWork.NumberIns.GetByIdAsync(id));
        if (numberIn == null)
            return NotFound();
        return Ok(numberIn);
    }

    [HttpPost("add")]
    [ActionName("add")]
    public async Task<IActionResult> AddNumberInAsync(NumberInDto data)
    {
        if (data == null)
            return BadRequest();
        var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberInDto, NumberIn>());
        var mapper = new Mapper(config);
        NumberIn numberIn = mapper.Map<NumberInDto, NumberIn>(data);

        _unitOfWork.NumberIns.Add(numberIn);
        await _unitOfWork.CompleteAsync();
        config = new MapperConfiguration(cfg => cfg.CreateMap<NumberIn, NumberInDto>());
        mapper = new Mapper(config);
        data = mapper.Map<NumberIn, NumberInDto>(numberIn);
        return Ok(data);

    }

    [HttpPut("update")]
    [ActionName("update")]
    public async Task<IActionResult> UpadateNumberInAsync(NumberInDto data)
    {
        if (data == null)
            return BadRequest();
        NumberIn numberIn = await _unitOfWork.NumberIns.GetByIdAsync(data.NumberId);
        if (numberIn == null)
            return NotFound();
        var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberInDto, NumberIn>());
        var mapper = new Mapper(config);
        numberIn = mapper.Map<NumberInDto, NumberIn>(data); ;

        //_unitOfWork.NumberIns.Update(numberIn);
        await _unitOfWork.CompleteAsync();
        return Ok(data);

    }

    [HttpDelete("{id}")]
    [ActionName("delete")]
    public async Task<IActionResult> DeleteNumberInAsync(int id)
    {
        NumberIn numberIn = await _unitOfWork.NumberIns.GetByIdAsync(id);
        if (numberIn == null)
            return NotFound();
        var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberIn, NumberInDto>());
        var mapper = new Mapper(config);
        var _numberIn = mapper.Map<NumberIn, NumberInDto>(numberIn);

        _unitOfWork.NumberIns.Remove(numberIn);
        await _unitOfWork.CompleteAsync();
        return Ok(_numberIn);
    }
}*/
}
