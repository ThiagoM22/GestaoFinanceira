using FluentValidation;
using GestaoFinanceira.DTOs.CategoriaLucro;

namespace GestaoFinanceira.Validators
{
    public class CategoriaLucroCreateDtoValidator : AbstractValidator<CategoriaLucroCreateDto>
    {
        public CategoriaLucroCreateDtoValidator()
        {
                RuleFor(c => c.Nome)
                    .NotEmpty().WithMessage("O nome da categoria de lucro é obrigatório.")
                    .MaximumLength(100).WithMessage("O nome da categoria de lucro não pode exceder 100 caracteres.");
                RuleFor(c => c.Descricao)  
                    .MaximumLength(250).WithMessage("A descrição da categoria de lucro não pode exceder 250 caracteres.");
        }
    }
}
