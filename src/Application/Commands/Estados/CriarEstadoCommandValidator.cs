using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Estados
{
    public class CriarEstadoCommandValidator : AbstractValidator<CriarEstadoCommand>
    {
        public CriarEstadoCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .MaximumLength(150);

            RuleFor(x => x.Uf)
                .NotEmpty()
                .WithMessage("Uf é obrigatório.")
                .Length(2)
                .WithMessage("UF deve ter 2 caracteres.");
        }
    }
}
