namespace SistemaMedico.Models
{
    public class TratamentoPacienteModel
    {
       public int Id { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime Created_at { get; set; }
        public bool Status { get; set; }
        public int PacienteId { get; set; }
        public int EtapaId { get; set; }
        public PacienteModel? Paciente { get; set; }
        public EtapaModel? Etapa { get; set; }
        public PagamentoModel? Pagamento { get; set; }
        public ICollection<ArquivosTratamentoPacienteModel>? Arquivos { get; set; }
        public ICollection<AuditoriaModel>? Auditorias { get; set; }
    }
}
