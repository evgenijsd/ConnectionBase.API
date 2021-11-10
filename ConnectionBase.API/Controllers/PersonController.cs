using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IGenericServiceAsync<Person, PersonDto> _personServiceAsync;
        public PersonController(IGenericServiceAsync<Person, PersonDto> personServiceAsync)
        {
            _personServiceAsync = personServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _personServiceAsync.GetAllAsync();
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
            var result = await _personServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(PersonDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _personServiceAsync.AddAsync(data);
            var id = result.PersonId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(PersonDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _personServiceAsync.UpdateAsync(data, data.PersonId);
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
            var result = await _personServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _personServiceAsync.DeleteAsync(id);
            return NoContent();
        }
    }
}
