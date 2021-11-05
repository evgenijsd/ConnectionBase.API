using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public PersonController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetPeopleAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<Person>, List<PersonDto>>(await _unitOfWork.People.GetAllAsync()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetPersonAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDto>());
            var mapper = new Mapper(config);

            var person = mapper.Map<Person, PersonDto>(await _unitOfWork.People.GetByIdAsync(id));
            if (person == null)
                return NotFound();
            return Ok(person);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddPersonAsync(PersonDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDto, Person>());
            var mapper = new Mapper(config);
            Person person = mapper.Map<PersonDto, Person>(data);

            _unitOfWork.People.Add(person);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDto>());
            mapper = new Mapper(config);
            data = mapper.Map<Person, PersonDto> (person);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadatePersonAsync(PersonDto data)
        {
            if (data == null)
                return BadRequest();
            Person person = await _unitOfWork.People.GetByIdAsync(data.PersonId);
            if (person == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDto, Person>());
            var mapper = new Mapper(config);
            person = mapper.Map<PersonDto, Person>(data, person); ;

            //_unitOfWork.People.Update(person);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeletePersonAsync(int id)
        {
            Person person = await _unitOfWork.People.GetByIdAsync(id);
            if (person == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDto>());
            var mapper = new Mapper(config);
            var _person = mapper.Map<Person, PersonDto>(person);

            _unitOfWork.People.Remove(person);
            await _unitOfWork.CompleteAsync();
            return Ok(_person);
        }

    }*/
}
