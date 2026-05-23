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

namespace Application.Queries.Linguagens
{
    public class ObterLinguagensHandler : IRequestHandler<ObterLinguagensQuery, Result<IReadOnlyCollection<LinguagemDto>>>
    {
        private readonly ILinguagemRepository _repository;

        public ObterLinguagensHandler(ILinguagemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IReadOnlyCollection<LinguagemDto>>> Handle(ObterLinguagensQuery query, CancellationToken cancellationToken)
        {
            var linguagens = await _repository.ObterTodosComFiltroAsync(query.nome, cancellationToken);

            var dtos = linguagens
                .Select(l => new LinguagemDto(l.Id, l.Nome, l.Tipo.ToString()))
                .ToList();

            return Result<IReadOnlyCollection<DTOs.LinguagemDto>>.Success(dtos);
        }
    }
}
