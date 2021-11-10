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
        private readonly ICrossServiceAsync<Cross, CrossDto> _crossServiceAsync;
        public CrossController(ICrossServiceAsync<Cross, CrossDto> crossServiceAsync)
        {
            _crossServiceAsync = crossServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _crossServiceAsync.GetAllAsync();
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
            var result = await _crossServiceAsync.GetByIdAsync(id);
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
            var result = await _crossServiceAsync.AddAsync(data);
            var id = result.CrossId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(CrossDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _crossServiceAsync.UpdateAsync(data, data.CrossId);
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
            var result = await _crossServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _crossServiceAsync.DeleteAsync(id);
            return NoContent();
        }
    }
}
