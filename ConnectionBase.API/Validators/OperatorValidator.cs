using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using FluentValidation;

namespace ConnectionBase.Domain.Validators
{
    public class OperatorValidator : AbstractValidator<OperatorDto>
    {
        public OperatorValidator()
        {
            RuleFor(x => x.OperatorName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
