using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Desenvolvedores
{
    public class ObterDesenvolvedoresHandler : IRequestHandler<ObterDesenvolvedoresQuery, Result<IReadOnlyCollection<DesenvolvedorDto>>>
    {
        private readonly IDesenvolvedorRepository _repository;
        public ObterDesenvolvedoresHandler(IDesenvolvedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IReadOnlyCollection<DesenvolvedorDto>>> Handle(ObterDesenvolvedoresQuery query, CancellationToken cancellationToken)
        {
            var devs = await _repository.ObterTodosComFiltroAsync(query.Nome, query.Senioridade, query.LinguagemId, cancellationToken);

            var dtos = devs.Select(d => new DesenvolvedorDto(
                d.Id,
                d.Nome,
                d.Email,
                d.Senioridade.ToString(),
                d.CidadeId,
                d.Observacoes,
                d.Linguagens
                    .Select(l => new LinguagemDto(l.Id, l.Nome, l.Tipo.ToString()))
                    .ToList()
            )).ToList();

            return Result<IReadOnlyCollection<DesenvolvedorDto>>.Success(dtos);
        }
    }
}
