using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class PagamentoEtapaMap : IEntityTypeConfiguration<PagamentoEtapaModel>
    {
        public void Configure(EntityTypeBuilder<PagamentoEtapaModel> builder)
        {
            builder.ToTable("PagamentoEtapas");

            builder.HasKey(de => de.Id);

            builder.Property(de => de.Id).HasColumnName("Id").IsRequired();
            builder.Property(de => de.PagamentoId).HasColumnName("PagamentoId").IsRequired();
            builder.Property(de => de.EtapaId).HasColumnName("EtapaId").IsRequired();
            builder.Property(e => e.Pago).HasColumnName("Pago").IsRequired();
            builder.Property(e => e.Valor).HasColumnName("Valor").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(t => t.UrlCheck).HasColumnName("UrlCheck").HasMaxLength(400);
        }
    }
}
