using AutoMapper;
using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectionBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public BuildingController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetBuildingsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Building, BuildingDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<Building>, List<BuildingDto>>(await _unitOfWork.Buildings.GetAllAsync()));
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetBuildingAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Building, BuildingDto>());
            var mapper = new Mapper(config);

            var building = mapper.Map<Building, BuildingDto>(await _unitOfWork.Buildings.GetByIdAsync(id));
            if (building == null)
                return NotFound();
            return Ok(building);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddBuildingAsync(BuildingDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BuildingDto, Building>());
            var mapper = new Mapper(config);
            Building building = mapper.Map<BuildingDto, Building>(data);

            _unitOfWork.Buildings.Add(building);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<Building, BuildingDto>());
            mapper = new Mapper(config);
            data = mapper.Map<Building, BuildingDto>(building);
            return Ok(data);
        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateBuildingAsync(BuildingDto data)
        {
            if (data == null)
                return BadRequest();
            Building building = await _unitOfWork.Buildings.GetByIdAsync(data.BuildingId);
            if (building == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BuildingDto, Building>());
            var mapper = new Mapper(config);
            building = mapper.Map<BuildingDto, Building>(data, building);

            //_unitOfWork.Buildings.Update(building);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteBuildingAsync(int id)
        {
            Building building = await _unitOfWork.Buildings.GetByIdAsync(id);
            if (building == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Building, BuildingDto>());
            var mapper = new Mapper(config);
            var _building = mapper.Map<Building, BuildingDto>(building);

            _unitOfWork.Buildings.Remove(building);
            await _unitOfWork.CompleteAsync();
            return Ok(_building);
        }
    }
}
