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
    public class OperatorController : ControllerBase
    {
        private readonly IGenericService<Operator, OperatorDto> _operatorServiceAsync;
        public OperatorController(IGenericService<Operator, OperatorDto> operatorServiceAsync)
        {
            _operatorServiceAsync = operatorServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _operatorServiceAsync.GetAllAsync();
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
            var result = await _operatorServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(OperatorDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _operatorServiceAsync.AddAsync(data);
            var id = result.OperatorId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(OperatorDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _operatorServiceAsync.UpdateAsync(data, data.OperatorId);
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
            var result = await _operatorServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _operatorServiceAsync.DeleteAsync(id);
            return NoContent();
        }
    }
}
