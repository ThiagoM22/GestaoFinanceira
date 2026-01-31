using FluentValidation;
using GestaoFinanceira.DTOs.Gasto;

namespace GestaoFinanceira.Validators
{
    public class GastoCreateDtoValidator : AbstractValidator<GastoCreateDto>
    {
        public GastoCreateDtoValidator()
        {
            RuleFor(g => g.Valor)
                .GreaterThan(0).WithMessage("O valor do gasto deve ser maior que zero.");
            RuleFor(g => g.Data)
                .NotEmpty().WithMessage("A data do gasto é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data do gasto não pode ser no futuro.");
        }
    }
}
