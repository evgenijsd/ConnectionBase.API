using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairController : ControllerBase
    {
        private readonly IGenericService<Pair, PairDto> _pairServiceAsync;
        public PairController(IGenericService<Pair, PairDto> pairServiceAsync)
        {
            _pairServiceAsync = pairServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _pairServiceAsync.GetAllAsync();
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
            var result = await _pairServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(PairDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _pairServiceAsync.UpdateAsync(data, data.PairId);
            if (result == null)
                return NotFound();
            return Accepted(data);
        }

        [HttpGet("cross/{cross}")]
        [ActionName("cross")]
        public async Task<IActionResult> GetCrossFindAsync(int cross)
        {
            if (cross <= 0)
                return BadRequest();
            var result = await _pairServiceAsync.GetAsync(x => x.Cross == cross);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("out/{id}")]
        [ActionName("out")]
        public async Task<IActionResult> GetOutFindAsync(int id)
        {
            if (id <= 0)
                return BadRequest();
            var result = await _pairServiceAsync.GetAsync(x => x.PairIn == id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

    }
}
