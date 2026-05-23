using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class DesenvolvedorConfiguration : IEntityTypeConfiguration<Desenvolvedor>
    {
        public void Configure(EntityTypeBuilder<Desenvolvedor> builder)
        {
            builder.ToTable("Desenvolvedores");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Senioridade)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion<string>();

            builder.Property(x => x.Observacoes)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.HasOne<Cidade>()
                .WithMany()
                .HasForeignKey(x => x.CidadeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Linguagens)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "DesenvolvedorLinguagens",
                    j => j.HasOne<Linguagem>()
                          .WithMany()
                          .HasForeignKey("LinguagemId"),
                    j => j.HasOne<Desenvolvedor>()
                          .WithMany()
                          .HasForeignKey("DesenvolvedorId")
                );

            builder.Navigation(x => x.Linguagens)
                .HasField("_linguagens");

            builder.HasQueryFilter(x => !x.Deletado);
        }
    }
}
