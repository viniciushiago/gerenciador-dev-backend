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

namespace Application.Commands.Linguagens
{
    public class AtualizarLinguagemHandler : IRequestHandler<AtualizarLinguagemCommand, Result<Linguagem>>
    {
        private readonly ILinguagemRepository _repository;  
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarLinguagemHandler(ILinguagemRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Linguagem>> Handle(AtualizarLinguagemCommand command, CancellationToken cancellationToken)
        {
            var linguagem = await _repository.ObterPorIdAsync(command.Id, cancellationToken);

            if (linguagem is null)
                return Result<Linguagem>.Failure("Linguagem não encontrada");

            linguagem.Atualizar(command.Nome, command.TipoLinguagem);
            _repository.Atualizar(linguagem);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<Linguagem>.Failure("Não foi possível atualizar a linguagem");

            return Result<Linguagem>.Success(linguagem);
        }
    }
}
