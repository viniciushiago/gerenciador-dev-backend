using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Desenvolvedores
{
    public record ObterDesenvolvedoresQuery(string? Nome = null, string? Senioridade = null, int? LinguagemId = null) 
        : IRequest<Result<IReadOnlyCollection<DesenvolvedorDto>>>;
}
