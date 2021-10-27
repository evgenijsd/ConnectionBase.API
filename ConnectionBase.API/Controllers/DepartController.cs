using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public DepartController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetDepartsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Depart, DepartDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<Depart>, List<DepartDto>>(await _unitOfWork.Departs.GetAllAsync()));
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetDepartAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Depart, DepartDto>());
            var mapper = new Mapper(config);

            var depart = mapper.Map<Depart, DepartDto>(await _unitOfWork.Departs.GetByIdAsync(id));
            if (depart == null)
                return NotFound();
            return Ok(depart);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddDepartAsync(DepartDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartDto, Depart>());
            var mapper = new Mapper(config);
            Depart depart = mapper.Map<DepartDto, Depart>(data);

            _unitOfWork.Departs.Add(depart);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<Depart, DepartDto>());
            mapper = new Mapper(config);
            data = mapper.Map<Depart, DepartDto>(depart);
            return Ok(data);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateDepartAsync(DepartDto data)
        {
            if (data == null)
                return BadRequest();
            Depart depart = await _unitOfWork.Departs.GetByIdAsync(data.DepartId);
            if (depart == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartDto, Depart>());
            var mapper = new Mapper(config);
            depart = mapper.Map<DepartDto, Depart>(data);

            //_unitOfWork.Departs.Update(depart);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteDepartAsync(int id)
        {
            Depart depart = await _unitOfWork.Departs.GetByIdAsync(id);
            if (depart == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Depart, DepartDto>());
            var mapper = new Mapper(config);
            var _depart = mapper.Map<Depart, DepartDto>(depart);

            _unitOfWork.Departs.Remove(depart);
            await _unitOfWork.CompleteAsync();
            return Ok(_depart);
        }
    }
}

