namespace SistemaMedico.Models
{
    public class TratamentoPacienteEtapaModel
    {
        public int Id { get; set; }
        public int TratamentoPacienteId { get; set; }
        public TratamentoPacienteModel TratamentoPaciente { get; set; }
        public int EtapaId { get; set; }
        public EtapaModel Etapa { get; set; }
        public bool Concluida { get; set; }
        public DateTime? DataConclusao { get; set; }
    }
}
