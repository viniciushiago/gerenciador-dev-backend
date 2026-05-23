using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Remover(T entity);
        Task<T?> ObterPorIdAsync(int id, CancellationToken cancellation = default);
        Task<IReadOnlyList<T>> ObterTodosAsync(CancellationToken cancellation = default);
        Task<bool> ExisteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default);
    }
}
