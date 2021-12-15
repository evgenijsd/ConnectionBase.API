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
    public class NumberOutValidator : AbstractValidator<NumberOutDto>
    {
        private readonly IGenericService<Operator, OperatorDto> _operatorService;

        public NumberOutValidator(IGenericService<Operator, OperatorDto> operatorService)
        {
            _operatorService = operatorService;

            RuleFor(x => x.Number_Out)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Operator)
                .MustAsync(BeUniqueAsync).WithMessage("Нет такого оператора");
        }

        private async Task<bool> BeUniqueAsync(int? operatorId, CancellationToken token) =>
            await _operatorService.GetByValidIdAsync((int)operatorId);
    }
}
