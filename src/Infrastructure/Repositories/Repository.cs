using Domain.Commons;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
            => _context = context;

        public void Adicionar(T entity)
            => _context.Set<T>().Add(entity);

        public void Atualizar(T entity)
            => _context.Set<T>().Update(entity);

        public void Remover(T entity)
            => entity.Deletar();

        public async Task<T?> ObterPorIdAsync(int id, CancellationToken cancellation = default)
            => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellation);

        public async Task<IReadOnlyList<T>> ObterTodosAsync(CancellationToken cancellation = default)
            => await _context.Set<T>().AsNoTracking().ToListAsync(cancellation);

        public async Task<bool> ExisteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default)
            => await _context.Set<T>().AnyAsync(predicate, cancellation);
    }
}
