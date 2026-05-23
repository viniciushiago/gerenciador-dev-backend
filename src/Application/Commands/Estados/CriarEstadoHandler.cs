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
    public class CriarEstadoHandler : IRequestHandler<CriarEstadoCommand, Result<Estado>>
    {
        private readonly IEstadoRepository _repository;
        private readonly IUnitOfWork _unit;
        public CriarEstadoHandler(IEstadoRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<Result<Estado>> Handle(CriarEstadoCommand command, CancellationToken cancellationToken)
        {
            var estado = Estado.Criar(command.Nome, command.Uf);
            _repository.Adicionar(estado);

            var sucesso = await _unit.CommitAsync();

            if (!sucesso)
                return Result<Estado>.Failure("Não foi possível salvar o estado.");

            return Result<Estado>.Success(estado);
        }
    }
}
