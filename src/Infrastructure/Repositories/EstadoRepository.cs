using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EstadoRepository : Repository<Estado>, IEstadoRepository
    {
        public EstadoRepository(AppDbContext context) : base(context) { }

        public async Task<Estado?> ObterPorIdComCidadesAsync(int id, CancellationToken ct = default)
        {
            return await _context.Estados
                .Include(e => e.Cidades)
                .FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task<IReadOnlyList<Estado>> ObterTodosComFiltroAsync(string? nome, CancellationToken ct = default)
        {
            var query = _context.Estados.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(e => e.Nome.ToLower().Contains(nome.ToLower()));

            return await query.AsNoTracking().ToListAsync(ct);
        }
    }
}
