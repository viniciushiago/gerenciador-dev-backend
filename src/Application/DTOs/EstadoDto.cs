using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record EstadoDto(int Id, string Nome, string Uf, IReadOnlyCollection<CidadeDto> Cidades);
}
