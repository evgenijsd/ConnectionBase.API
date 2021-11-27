﻿using ConnectionBase.API.DTO;
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
        private readonly IGenericService<DeviceModel, DeviceModelDto> _deviceModelServiceAsync;
        public DeviceModelController(IGenericService<DeviceModel, DeviceModelDto> deviceModelServiceAsync)
        {
            _deviceModelServiceAsync = deviceModelServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _deviceModelServiceAsync.GetAllAsync();
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
            var result = await _deviceModelServiceAsync.GetByIdAsync(id);
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
            var result = await _deviceModelServiceAsync.AddAsync(data);
            var id = result.ModelId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(DeviceModelDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _deviceModelServiceAsync.UpdateAsync(data, data.ModelId);
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
            var result = await _deviceModelServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _deviceModelServiceAsync.DeleteAsync(id);
            return NoContent();
        }
    }
}
