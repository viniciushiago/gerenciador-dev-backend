using Domain.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Linguagens
{
    public record DeletarLinguagemCommand(int Id) : IRequest<Result<bool>>;
}
