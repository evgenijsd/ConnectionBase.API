using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceModelController : ControllerBase
    {
        private readonly IGenericService<DeviceModel, DeviceModelDto> _deviceModelService;

        public DeviceModelController(IGenericService<DeviceModel, DeviceModelDto> deviceModelService)
        {
            _deviceModelService = deviceModelService;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _deviceModelService.GetAllAsync();
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
            var result = await _deviceModelService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(DeviceModelDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _deviceModelService.AddAsync(data);
            var id = result.ModelId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(DeviceModelDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _deviceModelService.UpdateAsync(data, data.ModelId);
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
            var result = await _deviceModelService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _deviceModelService.DeleteAsync(id);
            return NoContent();
        }
    }
}
