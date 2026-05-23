using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEstadoRepository : IRepository<Estado>
    {
        Task<Estado?> ObterPorIdComCidadesAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Estado>> ObterTodosComFiltroAsync(string? nome, CancellationToken ct = default);

    }
}
