using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class PacienteMap : IEntityTypeConfiguration<PacienteModel>
    {
        public void Configure(EntityTypeBuilder<PacienteModel> builder)
        {
            builder.ToTable("Pacientes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Codigo).HasColumnName("Codigo").IsRequired().HasMaxLength(6);
            builder.Property(p => p.Nome).HasColumnName("Nome").IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).HasColumnName("Email").IsRequired().HasMaxLength(100);
            builder.Property(p => p.Telefone).HasColumnName("Telefone").HasMaxLength(20);
            builder.Property(p => p.Cpf).HasColumnName("Cpf").IsRequired().HasMaxLength(14);
            builder.Property(p => p.Endereco).HasColumnName("Endereco").HasMaxLength(200);
            builder.HasIndex(p => p.Cpf).IsUnique();
            builder.HasIndex(p => p.Email).IsUnique();
        }
    }
}
