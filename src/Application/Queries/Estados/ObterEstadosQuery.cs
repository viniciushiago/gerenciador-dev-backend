using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Estados
{
    public record ObterEstadosQuery(string? Nome =  null) : IRequest<Result<IReadOnlyCollection<Estado>>>;
}
