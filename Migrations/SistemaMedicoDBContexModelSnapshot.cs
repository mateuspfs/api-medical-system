﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaMedico.Data;

#nullable disable

namespace SistemaMedico.Migrations
{
    [DbContext(typeof(SistemaMedicoDBContex))]
    partial class SistemaMedicoDBContexModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SistemaMedico.Models.AdminModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.ArquivosTratamentoPacienteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataUpload")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TratamentoPacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TratamentoPacienteId");

                    b.ToTable("ArquivosTratamentoPaciente");
                });

            modelBuilder.Entity("SistemaMedico.Models.AuditoriaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Acao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Acao");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataHora");

                    b.Property<int>("DoutorId")
                        .HasColumnType("int")
                        .HasColumnName("DoutorId");

                    b.Property<int>("TratamentoPacienteId")
                        .HasColumnType("int")
                        .HasColumnName("TratamentoPacienteId");

                    b.HasKey("Id");

                    b.HasIndex("DoutorId");

                    b.HasIndex("TratamentoPacienteId");

                    b.ToTable("Auditorias", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.DoutorEspecialidadeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DoutorId")
                        .HasColumnType("int")
                        .HasColumnName("DoutorId");

                    b.Property<int>("EspecialidadeId")
                        .HasColumnType("int")
                        .HasColumnName("EspecialidadeId");

                    b.HasKey("Id");

                    b.HasIndex("DoutorId");

                    b.HasIndex("EspecialidadeId");

                    b.ToTable("DoutorEspecialidades", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.DoutorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("DocumentoNome")
                        .IsRequired()
                        .HasMaxLength(220)
                        .HasColumnType("nvarchar(220)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(220)
                        .HasColumnType("nvarchar(220)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Doutores");
                });

            modelBuilder.Entity("SistemaMedico.Models.EspecialidadeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.ToTable("Especialidades", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.EtapaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Descricao");

                    b.Property<int?>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Titulo");

                    b.Property<int>("TratamentoId")
                        .HasMaxLength(300)
                        .HasColumnType("int")
                        .HasColumnName("TratamentoId");

                    b.HasKey("Id");

                    b.HasIndex("TratamentoId");

                    b.ToTable("Etapas", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.PacienteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("Codigo");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("Cpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Endereco");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Nome");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Pacientes", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.PagamentoEtapaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EtapaId")
                        .HasColumnType("int")
                        .HasColumnName("EtapaId");

                    b.Property<int>("PagamentoId")
                        .HasColumnType("int")
                        .HasColumnName("PagamentoId");

                    b.Property<bool>("Pago")
                        .HasColumnType("bit")
                        .HasColumnName("Pago");

                    b.Property<string>("UrlCheck")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnName("UrlCheck");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Valor");

                    b.HasKey("Id");

                    b.HasIndex("EtapaId");

                    b.HasIndex("PagamentoId");

                    b.ToTable("PagamentoEtapas", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.PagamentoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created_at");

                    b.Property<int>("TratamentoPacienteId")
                        .HasColumnType("int")
                        .HasColumnName("TratamentoPacienteId");

                    b.Property<DateTime?>("Updated_at")
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex("TratamentoPacienteId")
                        .IsUnique();

                    b.ToTable("Pagamentos", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.TratamentoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EspecialidadeId")
                        .HasColumnType("int")
                        .HasColumnName("EspecialidadeId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Nome");

                    b.Property<int>("Tempo")
                        .HasColumnType("int")
                        .HasColumnName("Tempo");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadeId");

                    b.ToTable("Tratamentos", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.TratamentoPacienteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created_at");

                    b.Property<int>("EtapaId")
                        .HasColumnType("int")
                        .HasColumnName("EtapaId");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Updated_at")
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex("EtapaId");

                    b.HasIndex("PacienteId");

                    b.ToTable("TratamentosPacientes", (string)null);
                });

            modelBuilder.Entity("SistemaMedico.Models.ArquivosTratamentoPacienteModel", b =>
                {
                    b.HasOne("SistemaMedico.Models.TratamentoPacienteModel", "TratamentoPaciente")
                        .WithMany("Arquivos")
                        .HasForeignKey("TratamentoPacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TratamentoPaciente");
                });

            modelBuilder.Entity("SistemaMedico.Models.AuditoriaModel", b =>
                {
                    b.HasOne("SistemaMedico.Models.DoutorModel", "Doutor")
                        .WithMany()
                        .HasForeignKey("DoutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaMedico.Models.TratamentoPacienteModel", "TratamentoPaciente")
                        .WithMany("Auditorias")
                        .HasForeignKey("TratamentoPacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doutor");

                    b.Navigation("TratamentoPaciente");
                });

            modelBuilder.Entity("SistemaMedico.Models.DoutorEspecialidadeModel", b =>
                {
                    b.HasOne("SistemaMedico.Models.DoutorModel", "Doutor")
                        .WithMany("DoutorEspecialidades")
                        .HasForeignKey("DoutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaMedico.Models.EspecialidadeModel", "Especialidade")
                        .WithMany("DoutorEspecialidades")
                        .HasForeignKey("EspecialidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doutor");

                    b.Navigation("Especialidade");
                });

            modelBuilder.Entity("SistemaMedico.Models.EtapaModel", b =>
                {
                    b.HasOne("SistemaMedico.Models.TratamentoModel", "Tratamento")
                        .WithMany("Etapas")
                        .HasForeignKey("TratamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tratamento");
                });

            modelBuilder.Entity("SistemaMedico.Models.PagamentoEtapaModel", b =>
                {
                    b.HasOne("SistemaMedico.Models.EtapaModel", "Etapa")
                        .WithMany()
                        .HasForeignKey("EtapaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaMedico.Models.PagamentoModel", "Pagamento")
                        .WithMany("PagamentoEtapas")
                        .HasForeignKey("PagamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etapa");

                    b.Navigation("Pagamento");
                });

            modelBuilder.Entity("SistemaMedico.Models.PagamentoModel", b =>
                {
                    b.HasOne("SistemaMedico.Models.TratamentoPacienteModel", "TratamentoPaciente")
                        .WithOne("Pagamento")
                        .HasForeignKey("SistemaMedico.Models.PagamentoModel", "TratamentoPacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TratamentoPaciente");
                });

            modelBuilder.Entity("SistemaMedico.Models.TratamentoModel", b =>
                {
                    b.HasOne("SistemaMedico.Models.EspecialidadeModel", "Especialidade")
                        .WithMany("Tratamentos")
                        .HasForeignKey("EspecialidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidade");
                });

            modelBuilder.Entity("SistemaMedico.Models.TratamentoPacienteModel", b =>
                {
                    b.HasOne("SistemaMedico.Models.EtapaModel", "Etapa")
                        .WithMany("TratamentoPaciente")
                        .HasForeignKey("EtapaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaMedico.Models.PacienteModel", "Paciente")
                        .WithMany("TratamentoPaciente")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etapa");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("SistemaMedico.Models.DoutorModel", b =>
                {
                    b.Navigation("DoutorEspecialidades");
                });

            modelBuilder.Entity("SistemaMedico.Models.EspecialidadeModel", b =>
                {
                    b.Navigation("DoutorEspecialidades");

                    b.Navigation("Tratamentos");
                });

            modelBuilder.Entity("SistemaMedico.Models.EtapaModel", b =>
                {
                    b.Navigation("TratamentoPaciente");
                });

            modelBuilder.Entity("SistemaMedico.Models.PacienteModel", b =>
                {
                    b.Navigation("TratamentoPaciente");
                });

            modelBuilder.Entity("SistemaMedico.Models.PagamentoModel", b =>
                {
                    b.Navigation("PagamentoEtapas");
                });

            modelBuilder.Entity("SistemaMedico.Models.TratamentoModel", b =>
                {
                    b.Navigation("Etapas");
                });

            modelBuilder.Entity("SistemaMedico.Models.TratamentoPacienteModel", b =>
                {
                    b.Navigation("Arquivos");

                    b.Navigation("Auditorias");

                    b.Navigation("Pagamento");
                });
#pragma warning restore 612, 618
        }
    }
}
