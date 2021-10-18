using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConnectionBase.API.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;

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
        public async Task<IActionResult> GetGenerateAsync() => Ok(await _chainService.GetAllChain());

        [HttpGet("list")]
        [ActionName("list")]
        public async Task<IActionResult> GetListAsync() => Ok(await _chainService.GetListChains());
    }
}
