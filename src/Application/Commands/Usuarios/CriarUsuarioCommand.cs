using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Usuarios
{
    public record CriarUsuarioCommand(string Nome, string Email, string Senha) : IRequest<Result<UsuarioDto>>;
}
