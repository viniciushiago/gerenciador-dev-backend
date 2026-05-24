using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Estados.Any())
            {
                var sc = Estado.Criar("Santa Catarina", "SC");
                var rj = Estado.Criar("Rio de Janeiro", "RJ");
                var mg = Estado.Criar("Minas Gerais", "MG");

                context.Estados.AddRange(sc, rj, mg);
                await context.SaveChangesAsync();

                var spCidade = Cidade.Criar("Itajaí", sc.Id);
                var rjCidade = Cidade.Criar("Rio de Janeiro", rj.Id);
                var mgCidade = Cidade.Criar("Belo Horizonte", mg.Id);

                context.Cidades.AddRange(spCidade, rjCidade, mgCidade);
                await context.SaveChangesAsync();
            }

            if (!context.Linguagens.Any())
            {
                context.Linguagens.AddRange(
                    Linguagem.Criar("C#", TipoLinguagem.BackEnd),
                    Linguagem.Criar("JavaScript", TipoLinguagem.FrontEnd),
                    Linguagem.Criar("TypeScript", TipoLinguagem.FrontEnd),
                    Linguagem.Criar("Python", TipoLinguagem.BackEnd),
                    Linguagem.Criar("React", TipoLinguagem.FrontEnd),
                    Linguagem.Criar("Flutter", TipoLinguagem.Mobile),
                    Linguagem.Criar("Docker", TipoLinguagem.DevOps),
                    Linguagem.Criar("PostgreSQL", TipoLinguagem.Database)
                );
                await context.SaveChangesAsync();
            }

            if (!context.Usuarios.Any())
            {
                var senhaHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");
                context.Usuarios.Add(Usuario.Criar("Admin", "admin@sistema.com", senhaHash));
                await context.SaveChangesAsync();
            }
        }
    }
}
