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

namespace Application.Queries.Desenvolvedores
{
    public class ObterDesenvolvedorPorIdHandler : IRequestHandler<ObterDesenvolvedorPorIdQuery, Result<DesenvolvedorDto>>
    {
        private readonly IDesenvolvedorRepository _repository;
        public ObterDesenvolvedorPorIdHandler(IDesenvolvedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<DesenvolvedorDto>> Handle(
    ObterDesenvolvedorPorIdQuery command,
    CancellationToken cancellationToken)
        {
            var dev = await _repository.ObterPorIdComLinguagensAsync(command.Id, cancellationToken);
            if (dev is null)
                return Result<DesenvolvedorDto>.Failure("Desenvolvedor não encontrado.");

            return Result<DesenvolvedorDto>.Success(new DesenvolvedorDto(
                dev.Id,
                dev.Nome,
                dev.Email,
                dev.Senioridade.ToString(),
                dev.CidadeId,
                dev.Observacoes,
                dev.Linguagens
                    .Select(l => new LinguagemDto(l.Id, l.Nome, l.Tipo.ToString()))
                    .ToList()
            ));
        }
    }
}
