using AutoMapper;
using ConnectionBase.API.Logic;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
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
    public class TableGeneratorController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        private ResultChain _resultChain;

        public TableGeneratorController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetGenerateAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, PairDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<IEnumerable<Pair>, List<PairDto>>(await _unitOfWork.Pairs.GetAllAsync()));
        }

    }
}
