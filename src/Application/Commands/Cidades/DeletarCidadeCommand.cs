using Domain.Commons;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Cidades
{
    public record DeletarCidadeCommand(int Id) : IRequest<Result<bool>>;
}
