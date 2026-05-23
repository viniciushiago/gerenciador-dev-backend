using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Cidades
{
    public class ObterCidadesHandler : IRequestHandler<ObterCidadesQuery, Result<IReadOnlyCollection<CidadeDto>>>
    {
        private readonly ICidadeRepository _repository;

        public ObterCidadesHandler(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IReadOnlyCollection<CidadeDto>>> Handle(ObterCidadesQuery query, CancellationToken cancellationToken)
        {
            var cidades = await _repository.ObterTodosComFiltroAsync(query.nome, cancellationToken);

            var dtos = cidades
                .Select(c => new CidadeDto(c.Id, c.Nome, c.EstadoId))
                .ToList();
            return Result<IReadOnlyCollection<CidadeDto>>.Success(dtos);
        }
    }
}
