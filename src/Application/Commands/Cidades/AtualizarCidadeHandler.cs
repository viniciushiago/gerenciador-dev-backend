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
    public class AtualizarCidadeHandler : IRequestHandler<AtualizarCidadeCommand, Result<Cidade>>
    {
        private readonly ICidadeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
public AtualizarCidadeHandler(ICidadeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Cidade>> Handle(AtualizarCidadeCommand command, CancellationToken cancellationToken)
        {
            var cidade = await _repository.ObterPorIdAsync(command.Id, cancellationToken);

            if (cidade is null)
                return Result<Cidade>.Failure("Cidade não encontrada.");

            cidade.Atualizar(command.Nome, cidade.EstadoId);
            _repository.Atualizar(cidade);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<Cidade>.Failure("Não foi possível atualizar a cidade.");

            return Result<Cidade>.Success(cidade);
        }
    }
}
