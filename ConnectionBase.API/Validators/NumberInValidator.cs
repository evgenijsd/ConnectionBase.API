using ConnectionBase.API.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionBase.API.Validators
{
    public class NumberInValidator : AbstractValidator<NumberInDto>
    {
        public NumberInValidator()
        {
            RuleFor(x => x.Number_In)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
