using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrossController : ControllerBase
    {
        private readonly ICrossService<Cross, CrossDto> _crossService;
        public CrossController(ICrossService<Cross, CrossDto> crossService)
        {
            _crossService = crossService;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _crossService.GetAllAsync();
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
            var result = await _crossService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(CrossDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _crossService.AddAsync(data);
            var id = result.CrossId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(CrossDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _crossService.UpdateAsync(data, data.CrossId);
            if (result == null)
                return NotFound();
            return Accepted(data);
        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteAync(int id)
        {
            if (id <= 0)
                return BadRequest();
            var result = await _crossService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _crossService.DeleteAsync(id);
            return NoContent();
        }
    }
}
