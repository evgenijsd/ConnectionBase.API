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
    public class CrossValidator : AbstractValidator<CrossDto>
    {
        private readonly IGenericService<Room, RoomDto> _roomService;

        public CrossValidator(IGenericService<Room, RoomDto> roomService)
        {
            _roomService = roomService;

            RuleFor(x => x.CrossName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Ats)
                .NotNull();
            RuleFor(x => x.NumberPair)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.BeginNum)
                .NotNull()
                .GreaterThanOrEqualTo(0);
           RuleFor(x => x.Room)
                .MustAsync(BeUnique).WithMessage("Нет такой комнаты");
        }

        private async Task<bool> BeUnique(int? roomId, CancellationToken token) =>
            await _roomService.GetByValidIdAsync((int)roomId);
    }
}
