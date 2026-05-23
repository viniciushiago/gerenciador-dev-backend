using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Usuarios
{
    public class DeletarUsuarioHandler : IRequestHandler<DeletarUsuarioCommand, Result<bool>>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarUsuarioHandler(IUsuarioRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeletarUsuarioCommand command, CancellationToken cancellationToken)
        {
            var usuario = await _repository.ObterPorIdAsync(command.Id);

            if (usuario is null)
                return Result<bool>.Failure("Usuário não encontrado.");

            _repository.Remover(usuario);
            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<bool>.Failure("Não foi possível deletar o usuário.");

            return Result<bool>.Success(true);
        }
    }
}
