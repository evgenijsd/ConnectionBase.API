using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConnectionBase.API.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using ConnectionBase.API.DTO;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableGeneratorController : ControllerBase
    {
        private readonly IChainService _chainService;

        public TableGeneratorController(IChainService chainService)
        {
            _chainService = chainService;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetGenerateAsync()
        {
            var Chains = await _chainService.GetAllChainAsync();
            if (Chains == null)
                return BadRequest();
            return Ok(Chains);
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetGenerateChainAsync(int id)
        {
            var Chain = await _chainService.GetChainAsync(id);
            if (Chain == null)
                return BadRequest();
            return Ok(Chain);
        }

        [HttpGet("list")]
        [ActionName("list")]
        public async Task<IActionResult> GetListAsync()
        {
            var listChains = await _chainService.GetListChainsAsync();
            if (listChains == null)
                return BadRequest();
            return Ok(listChains);
        }
    }
}
