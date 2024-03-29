﻿using ConnectionBase.API.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.Validators
{
    public class DepartValidator : AbstractValidator<DepartDto>
    {
        public DepartValidator()
        {
            RuleFor(x => x.DepartName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
