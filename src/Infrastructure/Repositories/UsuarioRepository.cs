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
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
