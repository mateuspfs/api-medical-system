    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SistemaMedico.Models;

    namespace SistemaMedico.Data.Map
    {
        public class DoutorMap : IEntityTypeConfiguration<DoutorModel>
        {
            public void Configure(EntityTypeBuilder<DoutorModel> builder)
            {
                builder.HasKey(x => x.Id); 
                builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
                builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
                builder.Property(x => x.Cpf).IsRequired().HasMaxLength(14);
                builder.Property(x => x.DocumentoNome).IsRequired().HasMaxLength(220);
                builder.Property(x => x.Endereco).HasMaxLength(220);
                builder.HasIndex(p => p.Cpf).IsUnique();
                builder.HasIndex(p => p.Email).IsUnique();
            }
        }
    }
