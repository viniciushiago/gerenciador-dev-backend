using Domain.Entities;
using Domain.Enums;
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
    public class DesenvolvedorRepository : Repository<Desenvolvedor>, IDesenvolvedorRepository
    {
        public DesenvolvedorRepository(AppDbContext context) : base(context) { }

        public async Task<Desenvolvedor?> ObterPorIdComLinguagensAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Desenvolvedores
                .Include(d => d.Linguagens)
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Desenvolvedor>> ObterTodosComLinguagensAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Desenvolvedores
                .Include(d => d.Linguagens)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Desenvolvedor>> ObterTodosComFiltroAsync(string? nome, string? senioridade, int? linguagemId, CancellationToken cancellationToken = default)
        {
            var query = _context.Desenvolvedores
                .Include(d => d.Linguagens)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(d => d.Nome.ToLower().Contains(nome.ToLower()));

            if (!string.IsNullOrWhiteSpace(senioridade) &&
                Enum.TryParse<Senioridade>(senioridade, ignoreCase: true, out var senioridadeEnum))
                query = query.Where(d => d.Senioridade == senioridadeEnum);

            if (linguagemId.HasValue)
                query = query.Where(d => d.Linguagens.Any(l => l.Id == linguagemId));

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task RemoverLinguagensAsync(int desenvolvedorId, CancellationToken ct = default)
        {
            var dev = await _context.Desenvolvedores
                .Include(d => d.Linguagens)
                .FirstOrDefaultAsync(d => d.Id == desenvolvedorId, ct);

            if (dev is null) return;

            dev.Linguagens
                .ToList()
                .ForEach(l => dev.RemoverLinguagem(l));
        }

    }
}
