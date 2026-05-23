using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Cidades
{
    public class CriarCidadeCommandValidator : AbstractValidator<CriarCidadeCommand>
    {
        public CriarCidadeCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.");

            RuleFor(x => x.EstadoId)
                .GreaterThan(0)
                .WithMessage("Estado é obrigatório.");
        }
    }
}
