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
    public class RoomValidator : AbstractValidator<RoomDto>
    {
        private readonly IGenericService<Building, BuildingDto> _buildingService;

        public RoomValidator(IGenericService<Building, BuildingDto> buildingService)
        {
            _buildingService = buildingService;

            RuleFor(x => x.RoomName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Building)
                .NotEmpty()
                .MustAsync(BeUniqueAsync).WithMessage("Нет такого сооружения"); ;
        }

        private async Task<bool> BeUniqueAsync(int buildingId, CancellationToken token) =>
            await _buildingService.GetByValidIdAsync(buildingId);
    }
}
