using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return Ok(mapper.Map<Depart, DepartDto>(_unitOfWork.Departs.GetById(id)));
        }

        [HttpPost("add")]
        [ActionName("add")]
        public IActionResult AddDepart(DepartDto data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartDto, Depart>());
            var mapper = new Mapper(config);
            Depart depart = mapper.Map<DepartDto, Depart>(data);

            _unitOfWork.Departs.Add(depart);
            _unitOfWork.Complete();
            return Ok();

        }

        [HttpPut("update")]
        [ActionName("update")]
        public IActionResult UpadateDepart(DepartDto data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartDto, Depart>());
            var mapper = new Mapper(config);
            Depart depart = mapper.Map<DepartDto, Depart>(data); ;

            _unitOfWork.Departs.Update(depart);
            _unitOfWork.Complete();
            return Ok();

        }
    }
}
