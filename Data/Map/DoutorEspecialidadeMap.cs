using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class DoutorEspecialidadeMap : IEntityTypeConfiguration<DoutorEspecialidadeModel>
    {
        public void Configure(EntityTypeBuilder<DoutorEspecialidadeModel> builder)
        {
            builder.ToTable("DoutorEspecialidades");

            builder.HasKey(de => de.Id);

            builder.Property(de => de.Id).HasColumnName("Id").IsRequired();
            builder.Property(de => de.DoutorId).HasColumnName("DoutorId").IsRequired();
            builder.Property(de => de.EspecialidadeId).HasColumnName("EspecialidadeId").IsRequired();
        }
    }
}