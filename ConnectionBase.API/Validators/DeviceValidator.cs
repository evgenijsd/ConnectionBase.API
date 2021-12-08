using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using ConnectionBase.Domain.Service.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionBase.API.Validators
{
    public class DeviceValidator : AbstractValidator<DeviceDto>
    {
        private readonly IGenericService<Room, RoomDto> _roomService;
        private readonly IGenericService<DeviceModel, DeviceModelDto> _deviceModelService;
        private readonly IGenericService<Pair, PairDto> _pairService;

        public DeviceValidator(IGenericService<Room, RoomDto> roomService, IGenericService<DeviceModel, DeviceModelDto> deviceModelService,
                               IGenericService<Pair, PairDto> pairService)
        {
            _roomService = roomService;
            _deviceModelService = deviceModelService;
            _pairService = pairService;

            RuleFor(x => x.Model)
                .NotEmpty()
                .MustAsync(BeUniqueModel).WithMessage("Нет такой модели");
            RuleFor(x => x.Room)
                .NotEmpty()
                .MustAsync(BeUniquePerson).WithMessage("Нет такой комнаты");
            RuleFor(x => x.Pair)
                .NotEmpty()
                .MustAsync(BeUniquePerson).WithMessage("Нет такой пары");
        }

        private async Task<bool> BeUniqueModel(int modelId, CancellationToken token) =>
            await _deviceModelService.GetByValidIdAsync(modelId);

        private async Task<bool> BeUniquePerson(int? personId, CancellationToken token) =>
            await _roomService.GetByValidIdAsync((int)personId);

        private async Task<bool> BeUniquePair(int? pairId, CancellationToken token) =>
            await _pairService.GetByValidIdAsync((int)pairId);
    }
}
