using Domain.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Estados
{
    public record DeletarEstadoCommand(int Id) : IRequest<Result<bool>>;
}
