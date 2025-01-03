namespace SistemaMedico.Models
{
    public class PacienteModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public ICollection<TratamentoPacienteModel>? TratamentoPaciente { get; set; }
    }
}
