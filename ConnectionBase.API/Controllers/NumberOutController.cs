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
    [Route("api/[controller]")]
    [ApiController]
    public class NumberOutController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public NumberOutController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetNumberOutsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberOut, NumberOutDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<NumberOut>, List<NumberOutDto>>(await _unitOfWork.NumberOuts.GetAllAsync()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetNumberOutAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberOut, NumberOutDto>());
            var mapper = new Mapper(config);

            var numberOut = mapper.Map<NumberOut, NumberOutDto>(await _unitOfWork.NumberOuts.GetByIdAsync(id));
            if (numberOut == null)
                return NotFound();
            return Ok(numberOut);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddNumberOutAsync(NumberOutDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberOutDto, NumberOut>());
            var mapper = new Mapper(config);
            NumberOut numberOut = mapper.Map<NumberOutDto, NumberOut>(data);

            _unitOfWork.NumberOuts.Add(numberOut);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<NumberOut, NumberOutDto>());
            mapper = new Mapper(config);
            data = mapper.Map<NumberOut, NumberOutDto>(numberOut);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateNumberOutAsync(NumberOutDto data)
        {
            if (data == null)
                return BadRequest();
            NumberOut numberOut = await _unitOfWork.NumberOuts.GetByIdAsync(data.NumberId);
            if (numberOut == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberOutDto, NumberOut>());
            var mapper = new Mapper(config);
            numberOut = mapper.Map<NumberOutDto, NumberOut>(data, numberOut); ;

            //_unitOfWork.NumberOuts.Update(numberOut);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteNumberOutAsync(int id)
        {
            NumberOut numberOut = await _unitOfWork.NumberOuts.GetByIdAsync(id);
            if (numberOut == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NumberOut, NumberOutDto>());
            var mapper = new Mapper(config);
            var _numberOut = mapper.Map<NumberOut, NumberOutDto>(numberOut);

            _unitOfWork.NumberOuts.Remove(numberOut);
            await _unitOfWork.CompleteAsync();
            return Ok(_numberOut);
        }
    }
}
