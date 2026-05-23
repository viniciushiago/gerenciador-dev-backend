using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Usuarios
{
    public class CriarUsuarioCommandValidator : AbstractValidator<CriarUsuarioCommand>
    {

        public CriarUsuarioCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório.")
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");
        }
    }
}
