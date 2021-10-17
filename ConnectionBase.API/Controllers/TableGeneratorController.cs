using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConnectionBase.API.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectionBase.Domain.Entities;

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
        public IActionResult GetGenerateAsync()
        {
            return Ok(_chainService.GetAllChain());
        }
    }
}
