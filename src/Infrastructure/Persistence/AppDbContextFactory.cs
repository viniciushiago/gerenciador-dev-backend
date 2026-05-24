using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=GerenciadorDev;User Id=sa;Password=Senha@123;TrustServerCertificate=True;"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}