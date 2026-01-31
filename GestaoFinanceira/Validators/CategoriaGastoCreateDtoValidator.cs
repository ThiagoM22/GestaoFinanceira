using FluentValidation;
using GestaoFinanceira.DTOs.CategoriaGasto;

namespace GestaoFinanceira.Validators
{
    public class CategoriaGastoCreateDtoValidator : AbstractValidator<CategoriaGastoCreateDto>
    {
        public CategoriaGastoCreateDtoValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome da categoria de gasto é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da categoria de gasto não pode exceder 100 caracteres.");

            RuleFor(c => c.Descricao)
                .MaximumLength(250).WithMessage("A descrição da categoria de gasto não pode exceder 250 caracteres.");
        }

    }
}
