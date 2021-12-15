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
    public class PairAbValidator : AbstractValidator<PairAbDto>
    {
        private readonly IGenericService<Pair, PairDto> _pairService;

        public PairAbValidator(IGenericService<Pair, PairDto> pairService)
        {
            _pairService = pairService;

            RuleFor(x => x.Pair)
                .NotEmpty()
                .MustAsync(BeUniqueAsync).WithMessage("Нет такой пары");
            RuleFor(x => x.BreakIn)
                .NotNull();
        }

        private async Task<bool> BeUniqueAsync(int pairId, CancellationToken token) =>
            await _pairService.GetByValidIdAsync(pairId);
    }
}
