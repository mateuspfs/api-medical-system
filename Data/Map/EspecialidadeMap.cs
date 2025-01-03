using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class EspecialidadeMap : IEntityTypeConfiguration<EspecialidadeModel>
    {
        public void Configure(EntityTypeBuilder<EspecialidadeModel> builder)
        {
            builder.ToTable("Especialidades");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
            builder.Property(e => e.Nome).HasColumnName("Nome").IsRequired().HasMaxLength(100);
        }
    }
}
