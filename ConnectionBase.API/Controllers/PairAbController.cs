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
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairAbController : ControllerBase
    {
        private readonly IGenericService<PairAb, PairAbDto> _pairAbService;

        public PairAbController(IGenericService<PairAb, PairAbDto> pairAbService)
        {
            _pairAbService = pairAbService;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _pairAbService.GetAllAsync();
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
            var result = await _pairAbService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(PairAbDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _pairAbService.AddAsync(data);
            var id = result.AbId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(PairAbDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _pairAbService.UpdateAsync(data, data.AbId);
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
            var result = await _pairAbService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _pairAbService.DeleteAsync(id);
            return NoContent();
        }
    }
}
