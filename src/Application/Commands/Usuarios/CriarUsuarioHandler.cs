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

namespace Application.Commands.Usuarios
{
    public class CriarUsuarioHandler : IRequestHandler<CriarUsuarioCommand, Result<UsuarioDto>>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarUsuarioHandler(IUsuarioRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UsuarioDto>> Handle(CriarUsuarioCommand command, CancellationToken cancellationToken)
        {
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(command.Senha);

            var emailExiste = await _repository.ExisteAsync(
                x => x.Email == command.Email, cancellationToken);
            if (emailExiste)
                return Result<UsuarioDto>.Failure("Já existe um usuário com esse e-mail.");

            var usuario = Usuario.Criar(command.Nome, command.Email, senhaHash);
            _repository.Adicionar(usuario);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<UsuarioDto>.Failure("Não foi possível criar o usuário.");

            return Result<UsuarioDto>.Success(new UsuarioDto(usuario.Id, usuario.Nome, usuario.Email));
        }
    }
}
