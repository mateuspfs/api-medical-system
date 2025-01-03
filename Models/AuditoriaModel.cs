namespace SistemaMedico.Models
{
    public class AuditoriaModel
    {
        public int Id { get; set; }
        public string Acao { get; set; }
        public DateTime DataHora { get; set; }
        public int TratamentoPacienteId { get; set; }
        public int DoutorId { get; set; }
        public TratamentoPacienteModel TratamentoPaciente { get; set; }
        public DoutorModel Doutor { get; set; }
    }
}
