using Application.Interfaces;
using Domain.Commons;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Login
{
    // Application/Commands/Usuarios/LoginHandler.cs
    public class LoginHandler : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUsuarioRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> Handle(
            LoginCommand command,
            CancellationToken cancellationToken)
        {
            var usuario = await _repository.ObterPorEmailAsync(command.Email, cancellationToken);
            if (usuario is null)
                return Result<string>.Failure("E-mail ou senha inválidos.");

            var senhaValida = BCrypt.Net.BCrypt.Verify(command.Senha, usuario.SenhaHash);
            if (!senhaValida)
                return Result<string>.Failure("E-mail ou senha inválidos.");

            var token = _tokenService.GerarToken(usuario);
            return Result<string>.Success(token);
        }
    }
}
