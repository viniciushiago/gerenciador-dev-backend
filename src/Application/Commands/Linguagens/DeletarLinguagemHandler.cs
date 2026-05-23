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
    public class DeletarLinguagemHandler : IRequestHandler<DeletarLinguagemCommand, Result<bool>>
    {
        private readonly ILinguagemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarLinguagemHandler(ILinguagemRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeletarLinguagemCommand command, CancellationToken cancellationToken)
        {
            var linguagem = await _repository.ObterPorIdAsync(command.Id, cancellationToken);

            if (linguagem is null)
                return Result<bool>.Failure("Linguagem não encontrada.");

            _repository.Remover(linguagem);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<bool>.Failure("Não foi possível deletar a linguagem.");

            return Result<bool>.Success(true);
        }
    }
}
