using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class TratamentoPacienteMap : IEntityTypeConfiguration<TratamentoPacienteModel>
    {
        public void Configure(EntityTypeBuilder<TratamentoPacienteModel> builder)
        {
            builder.ToTable("TratamentosPacientes");

            builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.Id).HasColumnName("Id").IsRequired();
            builder.Property(tp => tp.Updated_at).HasColumnName("Updated_at").IsRequired();
            builder.Property(tp => tp.Created_at).HasColumnName("Created_at").IsRequired();
            builder.Property(tp => tp.EtapaId).HasColumnName("EtapaId").IsRequired();
        }
    }
}
