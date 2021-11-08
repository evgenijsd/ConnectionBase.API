using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableGeneratorController : ControllerBase
    {
        private readonly IChainServiceAsync<GenerationListDto, GenerationChainsDto> _chainService;

        public TableGeneratorController(IChainServiceAsync<GenerationListDto, GenerationChainsDto> chainService)
        {
            _chainService = chainService;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetGenerateAsync()
        {
            var Chains = await _chainService.GetAllChainAsync();
            if (Chains == null)
                return NotFound();
            return Ok(Chains);
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetGenerateChainAsync(int id)
        {
            var Chain = await _chainService.GetChainAsync(id);
            if (Chain == null)
                return NotFound();
            return Ok(Chain);
        }

        [HttpGet("list")]
        [ActionName("list")]
        public async Task<IActionResult> GetListAsync()
        {
            var listChains = await _chainService.GetListChainsAsync();
            if (listChains == null)
                return NotFound();
            return Ok(listChains);
        }
    }
}
