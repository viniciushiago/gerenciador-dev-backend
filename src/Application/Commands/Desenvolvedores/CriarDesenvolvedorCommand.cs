using Domain.Commons;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Desenvolvedores
{
    public record CriarDesenvolvedorCommand(
        string Nome, 
        string Email, 
        Senioridade Senioridade,
        int CidadeId,
        string? Observacoes,
        List<int> LinguagensId
    ) : IRequest<Result<Desenvolvedor>>;
}
