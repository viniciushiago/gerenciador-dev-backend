using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Linguagens
{
    public record ObterLinguagemPorIdQuery(int Id) : IRequest<Result<LinguagemDto>>;
}
