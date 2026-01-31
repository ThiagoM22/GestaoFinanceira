using FluentValidation;
using GestaoFinanceira.DTOs.Lucro;

namespace GestaoFinanceira.Validators
{
    public class LucroCreateDtoValidator : AbstractValidator<LucroCreateDto>
    {
        public LucroCreateDtoValidator()
        {
            RuleFor(l => l.Valor)
                .GreaterThan(0).WithMessage("O valor do lucro deve ser maior que zero.");
            RuleFor(l => l.Data)
                .NotEmpty().WithMessage("A data do lucro é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data do lucro não pode ser no futuro.");
        }
    }
}
