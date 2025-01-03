namespace SistemaMedico.Models
{
    public class ArquivosTratamentoPacienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataUpload { get; set; }
        public int TratamentoPacienteId { get; set; }
        public TratamentoPacienteModel TratamentoPaciente { get; set; }
    }
}