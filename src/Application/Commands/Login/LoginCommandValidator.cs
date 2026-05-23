using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");
            
            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha obrigatória.");
        }
    }
}
