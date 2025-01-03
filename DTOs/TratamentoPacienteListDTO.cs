using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class TratamentoPacienteListDTO
    {
        public int Id { get; set; }
        public DateTime Created_at { get; set; }
        public bool Finalizado { get; set; }
        public PacienteDTO Paciente { get; set; }
        public TratamentoDTO Tratamento { get; set; }
        public EtapaDTO EtapaAtual { get; set; }
    }
}
