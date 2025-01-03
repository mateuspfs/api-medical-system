using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class EtapaMap : IEntityTypeConfiguration<EtapaModel>
    {
        public void Configure(EntityTypeBuilder<EtapaModel> builder)
        {
            builder.ToTable("Etapas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
            builder.Property(e => e.Titulo).HasColumnName("Titulo").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Descricao).HasColumnName("Descricao").IsRequired().HasMaxLength(300);
            builder.Property(e => e.TratamentoId).HasColumnName("TratamentoId").IsRequired().HasMaxLength(300);
        }
    }
}
