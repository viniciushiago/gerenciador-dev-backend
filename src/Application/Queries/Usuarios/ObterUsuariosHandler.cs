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
    public class ObterUsuariosHandler : IRequestHandler<ObterUsuariosQuery, Result<IReadOnlyCollection<UsuarioDto>>>
    {
        private readonly IUsuarioRepository _repository;

        public ObterUsuariosHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IReadOnlyCollection<UsuarioDto>>> Handle(ObterUsuariosQuery command, CancellationToken cancellationToken)
        {
            var usuarios = await _repository.ObterTodosAsync(cancellationToken);

            var dtos = usuarios
                .Select(u => new UsuarioDto(u.Id, u.Nome, u.Email))
                .ToList();
            return Result<IReadOnlyCollection<UsuarioDto>>.Success(dtos);
        }
    }
}
