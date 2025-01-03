namespace SistemaMedico.Models
{
    public class EtapaModel
    {
        public int Id { get; set; }
        public int? Numero { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int TratamentoId { get; set; }
        public TratamentoModel? Tratamento { get; set; }
        public ICollection<TratamentoPacienteModel>? TratamentoPaciente { get; set; }
    }
}
