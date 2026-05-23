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

namespace Application.Queries.Usuarios
{
    public class ObterUsuarioPorIdHandler : IRequestHandler<ObterUsuarioPorIdQuery, Result<UsuarioDto>>
    {
        private readonly IUsuarioRepository _repository;

        public ObterUsuarioPorIdHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<UsuarioDto>> Handle(ObterUsuarioPorIdQuery command, CancellationToken cancellationToken)
        {
            var usuario = await _repository.ObterPorIdAsync(command.Id);

            if (usuario is null)
                return Result<UsuarioDto>.Failure("Pessoa não encontrada.");

            return Result<UsuarioDto>.Success(new UsuarioDto(usuario.Id, usuario.Nome, usuario.Email));
        }
    }
}
