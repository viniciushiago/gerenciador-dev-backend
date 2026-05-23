using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Estados
{
    public class AtualizarEstadoHandler : IRequestHandler<AtualizarEstadoCommand, Result<Estado>>
    {
        private readonly IEstadoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarEstadoHandler(IEstadoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Estado>> Handle(AtualizarEstadoCommand command, CancellationToken cancellationToken)
        {
            var estado = await _repository.ObterPorIdAsync(command.Id, cancellationToken);

            if (estado is null)
                return Result<Estado>.Failure("Estado não encontrado.");

            var ufExiste = await _repository.ExisteAsync(
                x => x.Uf == command.Uf && x.Id != command.Id, cancellationToken);

            if (ufExiste)
                return Result<Estado>.Failure("Já existe um estado com essa UF.");

            estado.Atualizar(command.Nome, command.Uf);
            _repository.Atualizar(estado);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<Estado>.Failure("Não foi possível atualizar o estado.");

            return Result<Estado>.Success(estado);

        }
    }
}
