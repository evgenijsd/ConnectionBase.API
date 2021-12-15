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

        public DevicePersonValidator(IGenericService<Device, DeviceDto> deviceService, IGenericService<Person, PersonDto> personService)
        {
            _deviceService = deviceService;
            _personService = personService;

            RuleFor(x => x.Device)
                .NotEmpty()
                .MustAsync(BeUniqueDeviceAsync).WithMessage("Нет такого устройства");
            RuleFor(x => x.Person)
                .NotEmpty()
                .MustAsync(BeUniquePersonAsync).WithMessage("Нет такого сотрудника"); ;
        }

        private async Task<bool> BeUniqueDeviceAsync(int deviceId, CancellationToken token) =>
            await _deviceService.GetByValidIdAsync(deviceId);

        private async Task<bool> BeUniquePersonAsync(int? personId, CancellationToken token) =>
            await _personService.GetByValidIdAsync((int)personId);
    }
}
