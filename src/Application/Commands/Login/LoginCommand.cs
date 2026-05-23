using Domain.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Login
{
    public record LoginCommand(string Email, string Senha) : IRequest<Result<string>>;
}
