using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record DesenvolvedorDto(
        int Id,
        string Nome,
        string Email,
        string Senioridade,
        int CidadeId,
        string? Observacoes,
        IReadOnlyCollection<LinguagemDto> Linguagens
    );
}
