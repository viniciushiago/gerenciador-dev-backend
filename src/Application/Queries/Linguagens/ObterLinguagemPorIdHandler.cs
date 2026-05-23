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
    public class ObterLinguagemPorIdHandler : IRequestHandler<ObterLinguagemPorIdQuery, Result<LinguagemDto>>
    {
        private readonly ILinguagemRepository _repository;

        public ObterLinguagemPorIdHandler(ILinguagemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<LinguagemDto>> Handle(ObterLinguagemPorIdQuery query, CancellationToken cancellationToken)
        {
            var linguagem = await _repository.ObterPorIdAsync(query.Id, cancellationToken);

            if (linguagem is null)
                return Result<LinguagemDto>.Failure("Linguagem não encontrada.");

            return Result<LinguagemDto>.Success(new LinguagemDto(linguagem.Id, linguagem.Nome, linguagem.Tipo.ToString()));
        }
    }
}
