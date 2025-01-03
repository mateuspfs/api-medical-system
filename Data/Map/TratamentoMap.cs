using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaMedico.Models;

namespace SistemaMedico.Data.Map
{
    public class TratamentoMap : IEntityTypeConfiguration<TratamentoModel>
    {
        public void Configure(EntityTypeBuilder<TratamentoModel> builder)
        {
            builder.ToTable("Tratamentos");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
            builder.Property(t => t.Nome).HasColumnName("Nome").IsRequired().HasMaxLength(100);
            builder.Property(t => t.Tempo).HasColumnName("Tempo").IsRequired();
            builder.Property(t => t.EspecialidadeId).HasColumnName("EspecialidadeId").IsRequired();
        }
    }
}
