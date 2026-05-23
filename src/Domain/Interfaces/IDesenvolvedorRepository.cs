using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDesenvolvedorRepository : IRepository<Desenvolvedor>
    {
        Task<Desenvolvedor?> ObterPorIdComLinguagensAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Desenvolvedor>> ObterTodosComLinguagensAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Desenvolvedor>> ObterTodosComFiltroAsync(string? nome, string? senioridade, int? linguagemId, CancellationToken cancellationToken = default);
        Task RemoverLinguagensAsync(int desenvolvedorId, CancellationToken cancellationToken = default);
    }
}
