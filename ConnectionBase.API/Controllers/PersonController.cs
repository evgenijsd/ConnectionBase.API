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
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetPeople()
        {
            var listPeople = _unitOfWork.People.GetAll();
            return Ok(listPeople);

        }

        [HttpPost]
        public IActionResult AddPerson()
        {
            var person = new Person
            {
                PersonName = "Ансимова Елизавета Андреевна",
                Position = "секретарь"
            };

            _unitOfWork.People.Add(person);
            _unitOfWork.Complete();
            return Ok();

        }
    }
}
