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
    public class DevicePersonController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public DevicePersonController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetDevicePeopleAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DevicePerson, DevicePersonDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<DevicePerson>, List<DevicePersonDto>>(await _unitOfWork.DevicePeople.GetAllAsync()));

        }

        [HttpGet("device/{device}")]
        [ActionName("device")]
        public async Task<IActionResult> GetPeople(int device)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DevicePerson, DevicePersonDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<List<DevicePerson>, List<DevicePersonDto>>(await _unitOfWork.DevicePeople.FindAsync(x => x.Device == device)));
        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetDevicePersonAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DevicePerson, DevicePersonDto>());
            var mapper = new Mapper(config);

            var devicePerson = mapper.Map<DevicePerson, DevicePersonDto>(await _unitOfWork.DevicePeople.GetByIdAsync(id));
            if (devicePerson == null)
                return NotFound();
            return Ok(devicePerson);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddDevicePersonAsync(DevicePersonDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DevicePersonDto, DevicePerson>());
            var mapper = new Mapper(config);
            DevicePerson devicePerson = mapper.Map<DevicePersonDto, DevicePerson>(data);

            _unitOfWork.DevicePeople.Add(devicePerson);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<DevicePerson, DevicePersonDto>());
            mapper = new Mapper(config);
            data = mapper.Map<DevicePerson, DevicePersonDto>(devicePerson);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateDevicePersonAsync(DevicePersonDto data)
        {
            if (data == null)
                return BadRequest();
            DevicePerson devicePerson = await _unitOfWork.DevicePeople.GetByIdAsync(data.DevicePersonId);
            if (devicePerson == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DevicePersonDto, DevicePerson>());
            var mapper = new Mapper(config);
            devicePerson = mapper.Map<DevicePersonDto, DevicePerson>(data, devicePerson); ;

            //_unitOfWork.DevicePeople.Update(devicePerson);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteDevicePersonAsync(int id)
        {
            DevicePerson devicePerson = await _unitOfWork.DevicePeople.GetByIdAsync(id);
            if (devicePerson == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DevicePerson, DevicePersonDto>());
            var mapper = new Mapper(config);
            var _devicePerson = mapper.Map<DevicePerson, DevicePersonDto>(devicePerson);

            _unitOfWork.DevicePeople.Remove(devicePerson);
            await _unitOfWork.CompleteAsync();
            return Ok(_devicePerson);
        }

    }
}
