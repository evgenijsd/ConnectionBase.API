using ConnectionBase.API.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.Validators
{
    public class DeviceModelValidator : AbstractValidator<DeviceModelDto>
    {
        public DeviceModelValidator()
        {
            RuleFor(x => x.ModelName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
