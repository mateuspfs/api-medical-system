using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class AuditoriaMap : IEntityTypeConfiguration<AuditoriaModel>
    {
        public void Configure(EntityTypeBuilder<AuditoriaModel> builder)
        {
            builder.ToTable("Auditorias");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
            builder.Property(a => a.Acao).HasColumnName("Acao").IsRequired();
            builder.Property(a => a.DataHora).HasColumnName("DataHora").IsRequired();
            builder.Property(a => a.TratamentoPacienteId).HasColumnName("TratamentoPacienteId").IsRequired();
            builder.Property(a => a.DoutorId).HasColumnName("DoutorId").IsRequired();  
        }
    }
}
