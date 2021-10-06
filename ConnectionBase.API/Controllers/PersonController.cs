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

            var person = mapper.Map<Person, PersonDto>(_unitOfWork.People.GetById(id));
            if (person == null)
                return NotFound();
            return Ok(person);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public IActionResult AddPerson(PersonDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDto, Person>());
            var mapper = new Mapper(config);
            Person person = mapper.Map<PersonDto, Person>(data);

            _unitOfWork.People.Add(person);
            _unitOfWork.Complete();
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public IActionResult UpadatePerson(PersonDto data)
        {
            if (data == null)
                return BadRequest();
            if (_unitOfWork.People.Find(x => x.PersonId == data.PersonId) == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDto, Person>());
            var mapper = new Mapper(config);
            Person person = mapper.Map<PersonDto, Person>(data); ;

            _unitOfWork.People.Update(person);
            _unitOfWork.Complete();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public IActionResult DeletePerson(int id)
        {
            Person person = _unitOfWork.People.GetById(id);
            if (person == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDto>());
            var mapper = new Mapper(config);
            var _person = mapper.Map<Person, PersonDto>(person);

            _unitOfWork.People.Remove(person);
            _unitOfWork.Complete();
            return Ok(_person);
        }

    }
}
