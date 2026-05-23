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

namespace Application.Queries.Estados
{
    public class ObterEstadoPorIdHandler : IRequestHandler<ObterEstadoPorIdQuery, Result<EstadoDto>>
    {
        private readonly IEstadoRepository _repository;

        public ObterEstadoPorIdHandler(IEstadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<EstadoDto>> Handle(ObterEstadoPorIdQuery query, CancellationToken cancellationToken)
        {
            var estado = await _repository.ObterPorIdComCidadesAsync(query.Id, cancellationToken);

            if (estado is null)
                return Result<EstadoDto>.Failure("Estado não encontrado.");

            var cidadesDto = estado.Cidades
                .Select(c => new CidadeDto(c.Id, c.Nome, c.EstadoId))
                .ToList();

            return Result<EstadoDto>.Success(new EstadoDto(estado.Id, estado.Nome, estado.Uf, cidadesDto));
        }
    }
}
