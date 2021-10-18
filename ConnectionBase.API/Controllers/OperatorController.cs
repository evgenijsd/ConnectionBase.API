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
    public class OperatorController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public OperatorController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetOperatorsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Operator, OperatorDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<IEnumerable<Operator>, List<OperatorDto>>(await _unitOfWork.Operators.GetAllAsync()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetOperatorAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Operator, OperatorDto>());
            var mapper = new Mapper(config);

            var operator_ = mapper.Map<Operator, OperatorDto>(await _unitOfWork.Operators.GetByIdAsync(id));
            if (operator_ == null)
                return NotFound();
            return Ok(operator_);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddOperatorAsync(OperatorDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OperatorDto, Operator>());
            var mapper = new Mapper(config);
            Operator operator_ = mapper.Map<OperatorDto, Operator>(data);

            _unitOfWork.Operators.Add(operator_);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<Operator, OperatorDto>());
            mapper = new Mapper(config);
            data = mapper.Map<Operator, OperatorDto>(operator_);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateOperatorAsync(OperatorDto data)
        {
            if (data == null)
                return BadRequest();
            if (await _unitOfWork.Operators.AnyAsync(x => x.OperatorId == data.OperatorId) == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OperatorDto, Operator>());
            var mapper = new Mapper(config);
            Operator operator_ = mapper.Map<OperatorDto, Operator>(data); ;

            _unitOfWork.Operators.Update(operator_);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteOperatorAsync(int id)
        {
            Operator operator_ = await _unitOfWork.Operators.GetByIdAsync(id);
            if (operator_ == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Operator, OperatorDto>());
            var mapper = new Mapper(config);
            var _operator_ = mapper.Map<Operator, OperatorDto>(operator_);

            _unitOfWork.Operators.Remove(operator_);
            await _unitOfWork.CompleteAsync();
            return Ok(_operator_);
        }
    }
}
