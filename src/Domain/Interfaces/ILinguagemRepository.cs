using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILinguagemRepository : IRepository<Linguagem>
    {
        Task<IReadOnlyList<Linguagem>> ObterTodosComFiltroAsync(string? nome, CancellationToken cancellationToken = default);
    }
}
