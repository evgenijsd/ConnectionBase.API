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
    public class DevicePersonValidator : AbstractValidator<DevicePersonDto>
    {
        private readonly IGenericService<Device, DeviceDto> _deviceService;
        private readonly IGenericService<Person, PersonDto> _personService;

        public DevicePersonValidator(IGenericService<Device, DeviceDto> roomService, IGenericService<Person, PersonDto> personService)
        {
            RuleFor(x => x.Device)
                .NotEmpty();
            RuleFor(x => x.Person)
                .NotEmpty();
        }

        private async Task<bool> BeUniqueDevice(int deviceId, CancellationToken token) =>
            await _deviceService.GetByValidIdAsync(deviceId);

        private async Task<bool> BeUniquePerson(int? personId, CancellationToken token) =>
            await _personService.GetByValidIdAsync((int)personId);
    }
}
