﻿using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberInController : ControllerBase
    {
        private readonly IGenericService<NumberIn, NumberInDto> _numberInService;

        public NumberInController(IGenericService<NumberIn, NumberInDto> numberInService)
        {
            _numberInService = numberInService;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _numberInService.GetAllAsync();
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
            var result = await _numberInService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(NumberInDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _numberInService.AddAsync(data);
            var id = result.NumberId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(NumberInDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _numberInService.UpdateAsync(data, data.NumberId);
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
            var result = await _numberInService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _numberInService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("pair/{pair}")]
        [ActionName("pair")]
        public async Task<IActionResult> GetNumberInPairAsync(int pair)
        {
            if (pair <= 0)
                return BadRequest();
            var result = await _numberInService.GetAsync(x => x.PairAts == pair);
            if (result == null)
                return NotFound();
            return Ok(result);

        }

        [HttpGet("num/{num}")]
        [ActionName("num")]
        public async Task<IActionResult> GetNumberInFindAsync(string num)
        {
            if (num == string.Empty)
                return BadRequest();
            var result = await _numberInService.GetAsync(x => x.Number_In.Contains(num));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
