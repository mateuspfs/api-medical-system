using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class PagamentoMap : IEntityTypeConfiguration<PagamentoModel>
    {
        public void Configure(EntityTypeBuilder<PagamentoModel> builder)
        {
            builder.ToTable("Pagamentos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Created_at).HasColumnName("Created_at").IsRequired();
            builder.Property(p => p.Updated_at).HasColumnName("Updated_at").IsRequired();
            builder.Property(tp => tp.TratamentoPacienteId).HasColumnName("TratamentoPacienteId").IsRequired();
        }
    }
}
