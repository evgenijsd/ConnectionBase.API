using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService<Device, DeviceDto> _deviceServiceAsync;
        public DeviceController(IDeviceService<Device, DeviceDto> deviceServiceAsync)
        {
            _deviceServiceAsync = deviceServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _deviceServiceAsync.GetAllAsync();
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
            var result = await _deviceServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(DeviceDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _deviceServiceAsync.AddAsync(data);
            var id = result.DeviceId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(DeviceDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _deviceServiceAsync.UpdateAsync(data, data.DeviceId);
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
            var result = await _deviceServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _deviceServiceAsync.DeleteAsync(id);
            return NoContent();
        }
    }
}
