
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SistemaMedico.Data.Map;
using SistemaMedico.Models;

namespace SistemaMedico.Data
{
    public class SistemaMedicoDBContex : DbContext
    {
        public SistemaMedicoDBContex(DbContextOptions<SistemaMedicoDBContex> options)
            : base(options)
        {

        }

        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<DoutorModel> Doutores { get; set; }
        public DbSet<EspecialidadeModel> Especialidades { get; set; }
        public DbSet<EtapaModel> Etapas { get; set; }
        public DbSet<PagamentoModel> Pagamentos { get; set; }
        public DbSet<TratamentoModel> Tratamentos { get; set; }
        public DbSet<PacienteModel> Pacientes { get; set; }
        public DbSet<TratamentoPacienteModel> TratamentosPacientes { get; set; }
        public DbSet<DoutorEspecialidadeModel> DoutorEspecialidades { get; set; }
        public DbSet<AuditoriaModel> Auditorias { get; set; }
        public DbSet<ArquivosTratamentoPacienteModel> ArquivosTratamentoPaciente { get; set; }
        public DbSet<PagamentoEtapaModel> PagamentoEtapas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditoriaModel>()
                .HasOne(a => a.TratamentoPaciente)
                .WithMany()
                .HasForeignKey(a => a.TratamentoPacienteId);

            modelBuilder.Entity<TratamentoPacienteModel>()
                .HasOne(tp => tp.Pagamento)
                .WithOne(p => p.TratamentoPaciente)
                .HasForeignKey<PagamentoModel>(p => p.TratamentoPacienteId);

            modelBuilder.Entity<AuditoriaModel>()
                .HasOne(a => a.TratamentoPaciente)
                .WithMany(tp => tp.Auditorias)
                .HasForeignKey(a => a.TratamentoPacienteId);

            modelBuilder.ApplyConfiguration(new AdminMap());
            modelBuilder.ApplyConfiguration(new DoutorMap());
            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new DoutorEspecialidadeMap());
            modelBuilder.ApplyConfiguration(new PagamentoMap());
            modelBuilder.ApplyConfiguration(new EspecialidadeMap());
            modelBuilder.ApplyConfiguration(new TratamentoMap());
            modelBuilder.ApplyConfiguration(new TratamentoPacienteMap());
            modelBuilder.ApplyConfiguration(new EtapaMap());
            modelBuilder.ApplyConfiguration(new AuditoriaMap());
            modelBuilder.ApplyConfiguration(new PagamentoEtapaMap());
       
            base.OnModelCreating(modelBuilder);
        }
    }
}
