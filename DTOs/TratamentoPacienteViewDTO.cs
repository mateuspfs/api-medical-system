using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class TratamentoPacienteViewDTO
    {
        public int Id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public bool Finalizado { get; set; }
        public PacienteDTO Paciente { get; set; }
        public TratamentoDTO Tratamento { get; set; }
        public EtapaDTO EtapaAtual { get; set; }
        public PagamentoDTO Pagamento { get; set; }
        public List<AuditoriaDTO>? Auditorias { get; set; }
        public List<ArquivosTratamentoPacienteDTO>? Arquivos { get; set; }
    }
}
