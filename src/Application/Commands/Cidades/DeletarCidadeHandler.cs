using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Cidades
{
    public class DeletarCidadeHandler : IRequestHandler<DeletarCidadeCommand, Result<bool>>
    {
        private readonly ICidadeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarCidadeHandler(ICidadeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeletarCidadeCommand command, CancellationToken cancellationToken)
        {
            var cidade = await _repository.ObterPorIdAsync(command.Id);

            if (cidade is null)
                return Result<bool>.Failure("Cidada não encontrada.");

            _repository.Remover(cidade);
            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<bool>.Failure("Não foi possível deletar a cidade.");

            return Result<bool>.Success(true); 
        }
    }
}
