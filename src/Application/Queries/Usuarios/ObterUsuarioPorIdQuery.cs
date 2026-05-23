using Application.DTOs;
using Domain.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Usuarios
{
    public record ObterUsuarioPorIdQuery(int Id) : IRequest<Result<UsuarioDto>>;
}
