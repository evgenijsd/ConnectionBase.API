using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public IActionResult GetDeparts()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Depart, DepartDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<IEnumerable<Depart>, List<DepartDto>>(_unitOfWork.Departs.GetAll()));
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public IActionResult GetDepart(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Depart, DepartDto>());
            var mapper = new Mapper(config);

            var data = mapper.Map<Depart, DepartDto>(_unitOfWork.Departs.GetById(id));
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public IActionResult AddDepart(DepartDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartDto, Depart>());
            var mapper = new Mapper(config);
            Depart depart = mapper.Map<DepartDto, Depart>(data);

            _unitOfWork.Departs.Add(depart);
            _unitOfWork.Complete();
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public IActionResult UpadateDepart(DepartDto data)
        {
            if (data == null)
                return BadRequest();
            if (_unitOfWork.Departs.Find(x => x.DepartId == data.DepartId) == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartDto, Depart>());
            var mapper = new Mapper(config);
            Depart depart = mapper.Map<DepartDto, Depart>(data);

            _unitOfWork.Departs.Update(depart);
            _unitOfWork.Complete();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public IActionResult DeleteDepart(int id)
        {
            Depart depart = _unitOfWork.Departs.GetById(id);
            if (depart == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Depart, DepartDto>());
            var mapper = new Mapper(config);
            var _depart = mapper.Map<Depart, DepartDto>(depart);

            _unitOfWork.Departs.Remove(depart);
            _unitOfWork.Complete();
            return Ok(_depart);
        }
    }
}

