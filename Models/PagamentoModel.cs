    namespace SistemaMedico.Models
    {
        public class PagamentoModel
        {
            public int Id { get; set; }
            public DateTime Created_at { get; set; }
            public DateTime? Updated_at { get; set; }
            public int TratamentoPacienteId { get; set; }
            public TratamentoPacienteModel? TratamentoPaciente { get; set; }
            public ICollection<PagamentoEtapaModel>? PagamentoEtapas { get; set; }
        }
    }
