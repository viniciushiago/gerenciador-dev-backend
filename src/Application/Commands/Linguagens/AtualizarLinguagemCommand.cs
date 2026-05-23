using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Linguagens
{
    public record AtualizarLinguagemCommand(int Id, string Nome, TipoLinguagem TipoLinguagem) : IRequest<Result<Linguagem>>;
}
