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
    public class BuildingController : ControllerBase
    {
        private readonly IGenericServiceAsync<Building, BuildingDto> _buildingServiceAsync;
        public BuildingController(IGenericServiceAsync<Building, BuildingDto> buildingServiceAsync)
        {
            _buildingServiceAsync = buildingServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _buildingServiceAsync.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _buildingServiceAsync.GetByIdAsync(id); 
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(BuildingDto data)
        {
            if (data == null)
                return BadRequest();
            await _buildingServiceAsync.AddAsync(data);
            var id = data.BuildingId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateAsync(BuildingDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _buildingServiceAsync.UpadateAsync(data, data.BuildingId);
            if (result == 0)
                return NotFound();
            return Accepted(data);
        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> Deletesync(int id)
        {
            var result = await _buildingServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _buildingServiceAsync.DeleteAsync(id);
            return NoContent();
        }
    }
}
