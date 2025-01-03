namespace SistemaMedico.DTOs
{
    public class TratamentoPacienteAddDTO
    {
        public string Codigo { get; set; }
        public int TratamentoId { get; set; }
        public List<IFormFile>? Arquivos { get; set; }
    }
}
