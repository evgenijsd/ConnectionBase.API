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
    public class DeviceController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public DeviceController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetDevicesAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Device, DeviceDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<Device>, List<DeviceDto>>(await _unitOfWork.Devices.GetAllAsync()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetDeviceAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Device, DeviceDto>());
            var mapper = new Mapper(config);

            var device = mapper.Map<Device, DeviceDto>(await _unitOfWork.Devices.GetByIdAsync(id));
            if (device == null)
                return NotFound();
            return Ok(device);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddDeviceAsync(DeviceDto data)
        {
            if (data == null)
                return BadRequest();
            Pair pair = new() { PairNum = 0, Cross = null };
            _unitOfWork.Pairs.Add(pair);
            await _unitOfWork.CompleteAsync();
            data.Pair = pair.PairId;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeviceDto, Device>());
            var mapper = new Mapper(config);
            Device device = mapper.Map<DeviceDto, Device>(data);

            _unitOfWork.Devices.Add(device);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<Device, DeviceDto>());
            mapper = new Mapper(config);
            data = mapper.Map<Device, DeviceDto>(device);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateDeviceAsync(DeviceDto data)
        {
            if (data == null)
                return BadRequest();
            Device device = await _unitOfWork.Devices.GetByIdAsync(data.DeviceId);
            if (device == null)
                return NotFound();
            data.Pair = device.Pair;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeviceDto, Device>());
            var mapper = new Mapper(config);
            device = mapper.Map<DeviceDto, Device>(data);

            //_unitOfWork.Devices.Update(device);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteDeviceAsync(int id)
        {
            Device device = await _unitOfWork.Devices.GetByIdAsync(id);
            if (device == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Device, DeviceDto>());
            var mapper = new Mapper(config);
            var _device = mapper.Map<Device, DeviceDto>(device);

            Pair pair = await _unitOfWork.Pairs.GetByIdAsync((int)device.Pair);
            _unitOfWork.Pairs.Remove(pair);
            _unitOfWork.Devices.Remove(device);
            await _unitOfWork.CompleteAsync();
            return Ok(_device);
        }
    }
}
