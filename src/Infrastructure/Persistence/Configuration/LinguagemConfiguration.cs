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
    public class LinguagemConfiguration : IEntityTypeConfiguration<Linguagem>
    {
        public void Configure(EntityTypeBuilder<Linguagem> builder)
        {
            builder.ToTable("Linguagens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Tipo)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion<string>();

            builder.HasQueryFilter(x => !x.Deletado);
        }
    }
}
