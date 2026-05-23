using Domain.Commons;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Estados
{
    public record CriarEstadoCommand(string Nome, string Uf) : IRequest<Result<Estado>>;
}
