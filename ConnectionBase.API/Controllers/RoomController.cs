using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IGenericService<Room, RoomDto> _roomServiceAsync;
        public RoomController(IGenericService<Room, RoomDto> roomServiceAsync)
        {
            _roomServiceAsync = roomServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _roomServiceAsync.GetAllAsync();
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
            var result = await _roomServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(RoomDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _roomServiceAsync.AddAsync(data);
            var id = result.RoomId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(RoomDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _roomServiceAsync.UpdateAsync(data, data.RoomId);
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
            var result = await _roomServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _roomServiceAsync.DeleteAsync(id);
            return NoContent();
        }
    }
}
