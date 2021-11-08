using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartController : ControllerBase
    {
        private readonly IGenericServiceAsync<Depart, DepartDto> _departServiceAsync;
        public DepartController(IGenericServiceAsync<Depart, DepartDto> departServiceAsync)
        {
            _departServiceAsync = departServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _departServiceAsync.GetAllAsync();
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
            var result = await _departServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(DepartDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _departServiceAsync.AddAsync(data);
            var id = result.DepartId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(DepartDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _departServiceAsync.UpdateAsync(data, data.DepartId);
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
            var result = await _departServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _departServiceAsync.DeleteAsync(id);
            return NoContent();
        }
    }
}

