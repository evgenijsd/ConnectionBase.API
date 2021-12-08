using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicePersonController : ControllerBase
    {
        private readonly IGenericService<DevicePerson, DevicePersonDto> _devicePersonService;

        public DevicePersonController(IGenericService<DevicePerson, DevicePersonDto> devicePersonService)
        {
            _devicePersonService = devicePersonService;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _devicePersonService.GetAllAsync();
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
            var result = await _devicePersonService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(DevicePersonDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _devicePersonService.AddAsync(data);
            var id = result.DevicePersonId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(DevicePersonDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _devicePersonService.UpdateAsync(data, data.DevicePersonId);
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
            var result = await _devicePersonService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _devicePersonService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("device/{device}")]
        [ActionName("device")]
        public async Task<IActionResult> GetCrossFindAsync(int device)
        {
            if (device <= 0)
                return BadRequest();
            var result = await _devicePersonService.GetAsync(x => x.Device == device);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
