using AutoMapper;
using ConnectionBase.API.DTO;
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
    public class DeviceModelController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public DeviceModelController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetDeviceModelsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeviceModel, DeviceModelDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<DeviceModel>, List<DeviceModelDto>>(await _unitOfWork.DeviceModels.GetAllAsync()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetDeviceModelAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeviceModel, DeviceModelDto>());
            var mapper = new Mapper(config);

            var deviceModel = mapper.Map<DeviceModel, DeviceModelDto>(await _unitOfWork.DeviceModels.GetByIdAsync(id));
            if (deviceModel == null)
                return NotFound();
            return Ok(deviceModel);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddDeviceModelAsync(DeviceModelDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeviceModelDto, DeviceModel>());
            var mapper = new Mapper(config);
            DeviceModel deviceModel = mapper.Map<DeviceModelDto, DeviceModel>(data);

            _unitOfWork.DeviceModels.Add(deviceModel);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<DeviceModel, DeviceModelDto>());
            mapper = new Mapper(config);
            data = mapper.Map<DeviceModel, DeviceModelDto>(deviceModel);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateDeviceModelAsync(DeviceModelDto data)
        {
            if (data == null)
                return BadRequest();
            DeviceModel deviceModel = await _unitOfWork.DeviceModels.GetByIdAsync(data.ModelId);
            if (deviceModel == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeviceModelDto, DeviceModel>());
            var mapper = new Mapper(config);
            deviceModel = mapper.Map<DeviceModelDto, DeviceModel>(data, deviceModel); ;

            //_unitOfWork.DeviceModels.Update(deviceModel);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteDeviceModelAsync(int id)
        {
            DeviceModel deviceModel = await _unitOfWork.DeviceModels.GetByIdAsync(id);
            if (deviceModel == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeviceModel, DeviceModelDto>());
            var mapper = new Mapper(config);
            var _deviceModel = mapper.Map<DeviceModel, DeviceModelDto>(deviceModel);

            _unitOfWork.DeviceModels.Remove(deviceModel);
            await _unitOfWork.CompleteAsync();
            return Ok(_deviceModel);
        }
    }
}
