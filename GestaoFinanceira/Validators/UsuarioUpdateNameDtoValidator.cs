using FluentValidation;
using GestaoFinanceira.Services.Usuario;

namespace GestaoFinanceira.Validators
{
    public class UsuarioUpdateNameDtoValidator : AbstractValidator<UsuarioUpdateNameDto>
    {
        public UsuarioUpdateNameDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome não pode ser vazio.")
                .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.");
        }
    }
}
