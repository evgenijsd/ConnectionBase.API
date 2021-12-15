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
    public class PairValidator : AbstractValidator<PairDto>
    {
        private readonly IGenericService<Pair, PairDto> _pairService;

        public PairValidator(IGenericService<Pair, PairDto> pairService)
        {
            _pairService = pairService;

            RuleFor(x => x.PairNum)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.PairIn)
                .MustAsync(BeUniqueAsync).WithMessage("Нет такой пары");
        }

        private async Task<bool> BeUniqueAsync(int? pairId, CancellationToken token) =>
            await _pairService.GetByValidIdAsync((int)pairId);
    }
}
