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
    public class CidadeRepository : Repository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Cidade>> ObterTodosComFiltroAsync(string? nome, CancellationToken cancellationToken = default)
        {
            var query = _context.Cidades.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(e => e.Nome.ToLower().Contains(nome.ToLower()));

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
