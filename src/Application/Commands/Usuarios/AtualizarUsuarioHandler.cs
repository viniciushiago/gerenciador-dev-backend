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
    public class AtualizarUsuarioHandler : IRequestHandler<AtualizarUsuarioCommand, Result<Usuario>>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarUsuarioHandler(IUsuarioRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Usuario>> Handle(AtualizarUsuarioCommand command, CancellationToken cancellationToken)
        {
            var usuario = await _repository.ObterPorIdAsync(command.Id, cancellationToken);

            if (usuario is null)
                return Result<Usuario>.Failure("Usuário não encontrado.");

            var emailExiste = await _repository.ExisteAsync(
                    x => x.Email == command.Email && x.Id != command.Id, cancellationToken);
            if (emailExiste)
                return Result<Usuario>.Failure("Já existe um usuário com esse e-mail.");

            usuario.Atualizar(command.Nome, command.Email);
            _repository.Atualizar(usuario);
            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<Usuario>.Failure("Não foi possível atualizar o usuário.");

            return Result<Usuario>.Success(usuario);
        }
    }
}
