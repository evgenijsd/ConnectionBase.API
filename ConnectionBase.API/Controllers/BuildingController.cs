using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IGenericService<Building, BuildingDto> _buildingService;

        public BuildingController(IGenericService<Building, BuildingDto> buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _buildingService.GetAllAsync();
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
            var result = await _buildingService.GetByIdAsync(id);
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
            var result = await _buildingService.AddAsync(data);
            var id = result.BuildingId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(BuildingDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _buildingService.UpdateAsync(data, data.BuildingId);
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
            var result = await _buildingService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _buildingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
