using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Usuarios
{
    public record ObterUsuariosQuery : IRequest<Result<IReadOnlyCollection<UsuarioDto>>>;
}
