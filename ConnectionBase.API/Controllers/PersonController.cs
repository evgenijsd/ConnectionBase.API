using AutoMapper;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        public IActionResult GetPeople()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<IEnumerable<Person>, List<PersonDto>>(_unitOfWork.People.GetAll()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public IActionResult GetPerson(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<Person, PersonDto>(_unitOfWork.People.GetById(id)));
        }

        [HttpPost("add")]
        [ActionName("add")]
        public IActionResult AddPerson(PersonDto data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDto, Person>());
            var mapper = new Mapper(config);
            Person person = mapper.Map<PersonDto, Person>(data);

            _unitOfWork.People.Add(person);
            _unitOfWork.Complete();
            return Ok();

        }

        [HttpPut("update")]
        [ActionName("update")]
        public IActionResult UpadateDepart(PersonDto data)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDto, Person>());
            var mapper = new Mapper(config);
            Person person = mapper.Map<PersonDto, Person>(data); ;

            _unitOfWork.People.Update(person);
            _unitOfWork.Complete();
            return Ok();

        }


    }
}
