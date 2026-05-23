using Domain.Commons;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Estados
{
    public class DeletarEstadoHandler : IRequestHandler<DeletarEstadoCommand, Result<bool>>
    {
        private readonly IEstadoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarEstadoHandler(IEstadoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeletarEstadoCommand command, CancellationToken cancellationToken)
        {
            var estado = await _repository.ObterPorIdAsync(command.Id, cancellationToken);

            if (estado is null)
                return Result<bool>.Failure("Estado não encontrado.");

            _repository.Remover(estado);
            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<bool>.Failure("Não foi possível deletar o estado.");

            return Result<bool>.Success(true);
        }
    }
}
