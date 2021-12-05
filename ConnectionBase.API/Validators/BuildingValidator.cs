using ConnectionBase.API.DTO;
using ConnectionBase.Domain.Entities;
using FluentValidation;

namespace ConnectionBase.Domain.Validators
{
    public class BuildingValidator : AbstractValidator<BuildingDto>
    {
        public BuildingValidator()
        {
            RuleFor(x => x.BuildingName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
