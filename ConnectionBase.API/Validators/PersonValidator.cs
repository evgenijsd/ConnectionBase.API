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
    public class PersonValidator : AbstractValidator<PersonDto>
    {
        private readonly IGenericService<Depart, DepartDto> _departService;

        public PersonValidator(IGenericService<Depart, DepartDto> departService)
        {
            _departService = departService;

            RuleFor(x => x.PersonName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Position)
                .MaximumLength(150);
            RuleFor(x => x.Depart)
                .MustAsync(BeUnique).WithMessage("Нет такого отдела");
        }

        private async Task<bool> BeUnique(int? departId, CancellationToken token) =>
            await _departService.GetByValidIdAsync((int)departId);
    }
}
