using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberOutController : ControllerBase
    {
        private readonly IGenericServiceAsync<NumberOut, NumberOutDto> _numberOutServiceAsync;
        public NumberOutController(IGenericServiceAsync<NumberOut, NumberOutDto> numberOutServiceAsync)
        {
            _numberOutServiceAsync = numberOutServiceAsync;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _numberOutServiceAsync.GetAllAsync();
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
            var result = await _numberOutServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddAsync(NumberOutDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _numberOutServiceAsync.AddAsync(data);
            var id = result.NumberId;
            return Created($"{id}", id);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpdateAsync(NumberOutDto data)
        {
            if (data == null)
                return BadRequest();
            var result = await _numberOutServiceAsync.UpdateAsync(data, data.NumberId);
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
            var result = await _numberOutServiceAsync.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            await _numberOutServiceAsync.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet("pair/{pair}")]
        [ActionName("pair")]
        public async Task<IActionResult> GetNumberOutPairAsync(int pair)
        {
            if (pair <= 0)
                return BadRequest();
            var result = await _numberOutServiceAsync.GetAsync(x => x.PairAts == pair);
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
            var result = await _numberOutServiceAsync.GetAsync(x => x.Number_Out.Contains(num));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
