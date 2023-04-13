using CondominioAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CondominioAPI.Infrastructure.Data.Configurations
{
    public class CondominioConfiguration : IEntityTypeConfiguration<Condominio>
    {
        public void Configure(EntityTypeBuilder<Condominio> builder)
        {
            builder.ToTable("TB_Condominio");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .HasColumnName("COND_Nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.CNPJ)
                .HasColumnName("COND_CNPJ")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(c => c.Endereco)
                .HasColumnName("COND_Endereco")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.NumeroUnidades)
                .HasColumnName("COND_NumeroUnidades")
                .IsRequired();

            builder.Property(c => c.NumeroBlocos)
                .HasColumnName("COND_NumeroBlocos")
                .IsRequired(false);

            builder.Property(c => c.DataFundacao)
                .HasColumnName("COND_DataFundacao")
                .IsRequired();

            // Adicionando índice no campo Nome
            builder.HasIndex(c => c.Nome)
                .HasDatabaseName("IX_COND_Nome")
                .IsUnique();
        }
    }
}
