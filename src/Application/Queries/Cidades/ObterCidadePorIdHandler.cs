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
    public class ObterCidadePorIdHandler : IRequestHandler<ObterCidadePorIdQuery, Result<CidadeDto>>
    {
        private readonly ICidadeRepository _repository;

        public ObterCidadePorIdHandler(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<CidadeDto>> Handle(ObterCidadePorIdQuery command, CancellationToken cancellationToken)
        {
            var cidade = await _repository.ObterPorIdAsync(command.Id);
            if (cidade is null)
                return Result<CidadeDto>.Failure("Cidade não encontrada.");

            return Result<CidadeDto>.Success(new CidadeDto(cidade.Id, cidade.Nome, cidade.EstadoId));
        }
    }
}
