using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Desenvolvedores
{
    public class CriarDesenvolvedorCommandValidator : AbstractValidator<CriarDesenvolvedorCommand>
    {
        public CriarDesenvolvedorCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .MaximumLength(150)
                .WithMessage("O nome deve ter no máximo 150 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("O e-mail informado é inválido.");

            RuleFor(x => x.Senioridade)
                .IsInEnum()
                .WithMessage("Senioridade inváilida.");

            RuleFor(x => x.LinguagensId)
            .NotEmpty().WithMessage("Deve possuir ao menos uma linguagem.")
            .Must(l => l.Count > 0).WithMessage("Deve possuir ao menos uma linguagem.");
        }
    }
}
