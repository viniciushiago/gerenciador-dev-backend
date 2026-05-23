using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Linguagens
{
    public class CriarLinguagemCommandValidator : AbstractValidator<CriarLinguagemCommand>
    {
        public CriarLinguagemCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório");

            RuleFor(x => x.TipoLinguagem)
                .IsInEnum()
                .WithMessage("Tipo da linguagem é obrigatório.");
                
        }
    }
}
