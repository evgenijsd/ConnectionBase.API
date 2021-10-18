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
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public RoomController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        [ActionName("all")]
        public async Task<IActionResult> GetRoomsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDto>());
            var mapper = new Mapper(config);
            return Ok(mapper.Map<IEnumerable<Room>, List<RoomDto>>(await _unitOfWork.Rooms.GetAllAsync()));

        }

        [HttpGet("{id}")]
        [ActionName("id")]
        public async Task<IActionResult> GetRoomAsync(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDto>());
            var mapper = new Mapper(config);

            var room = mapper.Map<Room, RoomDto>(await _unitOfWork.Rooms.GetByIdAsync(id));
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        [HttpPost("add")]
        [ActionName("add")]
        public async Task<IActionResult> AddRoomAsync(RoomDto data)
        {
            if (data == null)
                return BadRequest();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RoomDto, Room>());
            var mapper = new Mapper(config);
            Room room = mapper.Map<RoomDto, Room>(data);

            _unitOfWork.Rooms.Add(room);
            await _unitOfWork.CompleteAsync();
            config = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDto>());
            mapper = new Mapper(config);
            data = mapper.Map<Room, RoomDto>(room);
            return Ok(data);

        }

        [HttpPut("update")]
        [ActionName("update")]
        public async Task<IActionResult> UpadateRoomAsync(RoomDto data)
        {
            if (data == null)
                return BadRequest();
            if (await _unitOfWork.Rooms.AnyAsync(x => x.RoomId == data.RoomId) == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RoomDto, Room>());
            var mapper = new Mapper(config);
            Room room = mapper.Map<RoomDto, Room>(data); ;

            _unitOfWork.Rooms.Update(room);
            await _unitOfWork.CompleteAsync();
            return Ok(data);

        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> DeleteRoomAsync(int id)
        {
            Room room = await _unitOfWork.Rooms.GetByIdAsync(id);
            if (room == null)
                return NotFound();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDto>());
            var mapper = new Mapper(config);
            var _room = mapper.Map<Room, RoomDto>(room);

            _unitOfWork.Rooms.Remove(room);
            await _unitOfWork.CompleteAsync();
            return Ok(_room);
        }

    }
}
